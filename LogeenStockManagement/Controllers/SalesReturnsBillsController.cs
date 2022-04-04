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
    public class SalesReturnsBillsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public SalesReturnsBillsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/SalesReturnsBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesReturnsBill>>> GetSalesReturnsBills()
        {
            return await _context.SalesReturnsBills.ToListAsync();
        }

        // GET: api/SalesReturnsBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesReturnsBill>> GetSalesReturnsBill(int id)
        {
            var salesReturnsBill = await _context.SalesReturnsBills.FindAsync(id);

            if (salesReturnsBill == null)
            {
                return NotFound();
            }

            return salesReturnsBill;
        }

        // PUT: api/SalesReturnsBills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesReturnsBill(int id, SalesReturnsBill salesReturnsBill)
        {
            if (id != salesReturnsBill.Id || IsSaleReturnBillDataNotValid(salesReturnsBill))
            {
                return BadRequest();
            }

            _context.Entry(salesReturnsBill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesReturnsBillExists(id))
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

        // POST: api/SalesReturnsBills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesReturnsBill>> PostSalesReturnsBill(SalesReturnsBill salesReturnsBill)
        {
            if (IsSaleReturnBillDataNotValid(salesReturnsBill))
            {
                return BadRequest();
            }

            SaleBill sale = _context.SaleBills.Find(salesReturnsBill.SaleBillId);
            Tax ReturnTax = _context.Taxes.Find(salesReturnsBill.TaxId);
            List<StockProduct> stockProducts = new List<StockProduct>();

            foreach (ReturnSaleProduct item in salesReturnsBill.ReturnSaleProducts)
            {
                SaleBillProduct saleProduct = _context.SaleBillProducts.Find(item.SaleProductId);

                Product productType = _context.Products.Find(saleProduct.ProductId);
                
                salesReturnsBill.NetMoney += item.AmountReturned * productType.SellingPrice;
                

                //return to Stock products table (test?)
                StockProduct stockProduct;                                       /*item.ProductionDate*/
                if (StockProductExists(saleProduct.ProductId, sale.StockId, saleProduct.ProductionDate, out stockProduct))
                {
                    stockProduct.Amount += item.AmountReturned;
                }
                else
                {
                    stockProduct = stockProducts.Find(sp => sp.StockId == sale.StockId &&
                      sp.ProductId == saleProduct.ProductId && sp.ProductionDate == saleProduct.ProductionDate);

                    if (stockProduct != null)
                    {
                        stockProduct.Amount += item.AmountReturned;
                    }
                    else
                    {
                        stockProducts.Add(new StockProduct
                        {
                            ProductId = saleProduct.ProductId,
                            StockId = sale.StockId,
                            Amount = item.AmountReturned,
                            ProductionDate = saleProduct.ProductionDate
                        });

                    }
                }

            }

            salesReturnsBill.NetMoney -= salesReturnsBill.NetMoney / 100 * ReturnTax.Percentage;

            Client client = _context.Clients.Find(sale.ClientId);
            if (salesReturnsBill.NetMoney > client.BalanceOutstand)
            {
                PaymentMethod payment = _context.PaymentMethods.Find(salesReturnsBill.PayMethodId);
                payment.Balance -= salesReturnsBill.NetMoney - client.BalanceOutstand;
                client.BalanceOutstand = 0;
            }
            else
            {
                client.BalanceOutstand -= salesReturnsBill.NetMoney;
            }


            _context.SalesReturnsBills.Add(salesReturnsBill);

            _context.StockProducts.AddRange(stockProducts);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesReturnsBill", new { id = salesReturnsBill.Id }, salesReturnsBill);
        }

        // DELETE: api/SalesReturnsBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesReturnsBill(int id)
        {
            var salesReturnsBill = await _context.SalesReturnsBills.FindAsync(id);
            if (salesReturnsBill == null)
            {
                return NotFound();
            }

            _context.SalesReturnsBills.Remove(salesReturnsBill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesReturnsBillExists(int id)
        {
            return _context.SalesReturnsBills.Any(e => e.Id == id);
        }

        protected bool IsSaleReturnBillDataNotValid(SalesReturnsBill saleReturnBill)
        {
            //foreinkey
            bool SaleBillExisted = _context.SaleBills.Any(p => p.Id == saleReturnBill.SaleBillId);
            bool TaxExisted = _context.Taxes.Any(p => p.Id == saleReturnBill.TaxId);
            bool PayMethodExisted = _context.PaymentMethods.Any(p => p.Id == saleReturnBill.PayMethodId);

            //uniqe
            bool CodeRepeat = _context.SalesReturnsBills.Any((p) => p.Code == saleReturnBill.Code && p.Id != SaleReturnBill.Id);

            //Not NUll properties + chech Foreign and Uniqe result
            if (
                saleReturnBill.Code == null ||
                !SaleBillExisted || !TaxExisted || !PayMethodExisted ||
                CodeRepeat || !DateTime.TryParse(saleReturnBill.Date.ToString(), out _)
                || saleReturnBill.ReturnSaleProducts.Count == 0
                )
            {
                return true;
            }
            else
            {
                foreach (ReturnSaleProduct item in saleReturnBill.ReturnSaleProducts)
                {
                    if (!_context.SaleBillProducts.Any(sp => sp.Id == item.SaleProductId && sp.AmountToSell >= item.AmountReturned))
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
