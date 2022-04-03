using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogeenStockManagement.Models;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseBillsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public PurchaseBillsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseBill>>> GetPurchaseBills()
        {
            return await _context.PurchaseBills.ToListAsync();
        }

        // GET: api/PurchaseBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseBill>> GetPurchaseBill(int id)
        {
            var purchaseBill = await _context.PurchaseBills.FindAsync(id);

            if (purchaseBill == null)
            {
                return NotFound();
            }

            return purchaseBill;
        }

        // PUT: api/PurchaseBills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseBill(int id, PurchaseBill purchaseBill)
        {
            //validation section1

            if (id != purchaseBill.Id || IsPurchaseBillDataNotValid(purchaseBill))
            {
                return BadRequest();
            }

            _context.Entry(purchaseBill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseBillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PurchaseBills
        [HttpPost]
        public async Task<ActionResult<PurchaseBill>> PostPurchaseBill(PurchaseBill purchaseBill)
        {
            //validation section2
            if (IsPurchaseBillDataNotValid(purchaseBill))
            {
                return BadRequest(new {
                    ErrorStatus = "Validation",
                        Data = "",
                        Msg = "invalid Purchase bill Data."
                    });

            }


            List<StockProduct> stockProducts= new List<StockProduct>();
            foreach (PurchaseProduct item in purchaseBill.PurchaseProducts)
            {
                Product productType = _context.Products.Find(item.ProductId);

                DateTime ExpireDate = item.ProductionDate;

                ExpireDate.AddMonths(productType.ExpiryPeriod);

                if (ExpireDate <= DateTime.Today)
                {
                    return BadRequest(new
                    {
                        ErrorStatus = "Expired Product",
                        Data = item,
                        Msg = productType.Name + " with Production date: " + item.ProductionDate + "  Expired."
                    });
                }
                item.TotalPrice = item.Amount * productType.PurchasingPrice;

                purchaseBill.BillTotal += item.TotalPrice;

                //Add to Stock products table test?
                StockProduct stockProduct;
                if (StockProductExists(item.ProductId, purchaseBill.StockId, item.ProductionDate, out stockProduct))
                {
                    stockProduct.Amount += item.Amount;
                }
                else
                {
                    stockProduct = stockProducts.Find(sp => sp.StockId == purchaseBill.StockId &&
                      sp.ProductId == item.ProductId && sp.ProductionDate == item.ProductionDate);

                    if (stockProduct != null)
                    {
                        stockProduct.Amount += item.Amount;
                    }
                    else
                    {
                        stockProducts.Add(new StockProduct
                        {
                            ProductId = item.ProductId,
                            StockId = purchaseBill.StockId,
                            Amount = item.Amount,
                            ProductionDate = item.ProductionDate
                        });

                    }
                   
                }

            }

            

            purchaseBill.BillTotal -= purchaseBill.Discount;

            Tax billTax = _context.Taxes.Find(purchaseBill.TaxId);
            purchaseBill.BillTotal += purchaseBill.BillTotal / 100 * billTax.Percentage;

            purchaseBill.Remaining = purchaseBill.BillTotal - purchaseBill.Paidup;

            PaymentMethod payment = _context.PaymentMethods.Find(purchaseBill.PayMethodId);
            if (payment.Balance < purchaseBill.Paidup)
            {
                return BadRequest();
            }
            payment.Balance -= purchaseBill.Paidup;

            Supplier supplier = _context.Suppliers.Find(purchaseBill.SupplierId);
            supplier.BalanceDebit += purchaseBill.Remaining;


            _context.PurchaseBills.Add(purchaseBill);

            _context.StockProducts.AddRange(stockProducts);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseBill", new { id = purchaseBill.Id }, purchaseBill);
        }

        // DELETE: api/PurchaseBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseBill(int id)
        {
            var purchaseBill = await _context.PurchaseBills.FindAsync(id);
            if (purchaseBill == null)
            {
                return NotFound();
            }
            //validation section3
            if (
                purchaseBill.ExportPayments.Count > 0 ||
                purchaseBill.PurchaseProducts.Count > 0 ||
                purchaseBill.PurchaseReturnsBills.Count > 0
                )
            {
                return BadRequest();
            }

            _context.PurchaseBills.Remove(purchaseBill);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        // GET: api/PurchaseBills/id/5/products
        [Route("id/{billId}/Products")]
        [HttpGet]
        public IEnumerable<PurchaseProduct> GetPurchaseProductsByBillId(int billId)
        {
            return  _context.PurchaseProducts.Where(p=>p.PurchaseBillId==billId).ToList();
        }

        // GET: api/PurchaseBills/code/a5bc/products
        [Route("code/{billCode}/Products")]
        [HttpGet]
        public IEnumerable<PurchaseProduct> GetPurchaseProductsByBillCode(string billCode)
        {
            int BillId = _context.PurchaseBills.Where(b => b.BillCode == billCode).Select(b => b.Id).FirstOrDefault();
            return _context.PurchaseProducts.Where(p => p.PurchaseBillId == BillId).Select(p => p).ToList();
            
        }




        private bool PurchaseBillExists(int id)
        {
            return _context.PurchaseBills.Any(e => e.Id == id);
        }
        protected bool IsPurchaseBillDataNotValid(PurchaseBill purchaseBill)
        {
            //foreign keys can not refer to Not Existed
            bool StockExisted = _context.Stocks.Any(s => s.Id == purchaseBill.StockId);
            bool taxExisted = _context.Taxes.Any(t => t.Id == purchaseBill.TaxId);
            bool PayMethodExisted = _context.PaymentMethods.Any(p => p.Id == purchaseBill.PayMethodId);
            bool SupplierExisted = _context.Suppliers.Any(s => s.Id == purchaseBill.SupplierId);

            // UNIQUE Prorerty must to be Not Existed before
            bool BillCodeRepeat = _context.PurchaseBills.Any((b) => b.BillCode == purchaseBill.BillCode && b.Id != purchaseBill.Id);
            bool BillTypevalid = purchaseBill.BillType == "Cash" || purchaseBill.BillType == "Debit";

            //Not NUll properties + chech Foreign and Uniqe result
            if (
                purchaseBill.BillCode == null || purchaseBill.CheckNumber == null
                || !StockExisted || !taxExisted || !PayMethodExisted || !SupplierExisted
                || !BillTypevalid || BillCodeRepeat
                || !DateTime.TryParse(purchaseBill.Date.ToString(), out _)
                || purchaseBill.PurchaseProducts.Count == 0 
                //|| purchaseBill.BillTotal == 0
                )
            {
                    return true;
            }
            else
            {

                foreach (PurchaseProduct item in purchaseBill.PurchaseProducts)
                {
                    if (item.Amount <= 0 || !_context.Products.Any(p=>p.Id==item.ProductId))
                    {
                        return true;
                    }
                }

                return false;
            }

        }

        protected bool StockProductExists(int ProductId, int StockId, DateTime ProductionDate, out StockProduct stockProduct)
        {
            stockProduct = _context.StockProducts.
                 Where(sp => sp.StockId == StockId
                 && sp.ProductId == ProductId
                 && sp.ProductionDate == ProductionDate).FirstOrDefault();

            if (stockProduct == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
