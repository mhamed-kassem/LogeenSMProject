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
    public class SaleBillsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public SaleBillsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/SaleBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleBill>>> GetSaleBills()
        {
            return await _context.SaleBills.ToListAsync();
        }

        // GET: api/SaleBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleBill>> GetSaleBill(int id)
        {
            var saleBill = await _context.SaleBills.FindAsync(id);

            if (saleBill == null)
            {
                return NotFound();
            }

            return saleBill;
        }

        // PUT: api/SaleBills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleBill(int id, SaleBill saleBill)
        {
            if (id != saleBill.Id|| IsSaleBillDataNotValid(saleBill))
            {
                return BadRequest();
            }

            _context.Entry(saleBill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleBillExists(id))
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

        // POST: api/SaleBills
        [HttpPost]
        public async Task<ActionResult<SaleBill>> PostSaleBill(SaleBill saleBill)
        {
            if (IsSaleBillDataNotValid(saleBill))
            {
                return BadRequest();
            }


            foreach (SaleBillProduct item in saleBill.SaleBillProducts)
            {
                item.Discount = item.Product.Discount.UnitCount <= item.AmountToSell && item.Product.Discount.EndDate > DateTime.Today ? item.Product.Discount.DiscountValue : 0;
                item.TotalPrice = item.AmountToSell * item.Product.SellingPrice - (double)item.Discount;

                saleBill.BillTotalPrice += item.TotalPrice;

                //drop from Stock products table (test?)
                StockProduct stockProduct;                                       /*item.ProductionDate*/
                if (StockProductExists(item.ProductId, item.SaleBill.StockId, item.ProductionDate, out stockProduct))
                {
                    //stockProduct.Amount -= item.Amount;
                    if (stockProduct.Amount > item.AmountToSell)
                    {
                        stockProduct.Amount -= item.AmountToSell;

                    }
                    else if (stockProduct.Amount == item.AmountToSell)
                    {
                        _context.StockProducts.Remove(stockProduct);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }

            saleBill.Discount = saleBill.Client.Discount.EndDate > DateTime.Today ? saleBill.Client.Discount.DiscountValue : 0; 

            saleBill.BillTotalPrice -= saleBill.Discount;
            saleBill.BillTotalPrice += saleBill.BillTotalPrice / 100 * saleBill.Tax.Percentage;

            saleBill.Remaining = saleBill.BillTotalPrice - saleBill.Paidup;

            saleBill.PayMethod.Balance += saleBill.Paidup;

            saleBill.Client.BalanceOutstand += saleBill.Remaining;


            _context.SaleBills.Add(saleBill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleBill", new { id = saleBill.Id }, saleBill);

        }

        //TODO remove delete functionality
        // DELETE: api/SaleBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleBill(int id)
        {
            var saleBill = await _context.SaleBills.FindAsync(id);
            if (saleBill == null)
            {
                return NotFound();
            }

            if (saleBill.ImportPayments.Count > 0 ||
                saleBill.SaleBillProducts.Count > 0 ||
                saleBill.SalesReturnsBills.Count > 0
               )
            {
                return BadRequest();
            }

            _context.SaleBills.Remove(saleBill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/SaleBills/id/5/products
        [Route("id/{billId}/SaleProducts")]
        [HttpGet]
        public IEnumerable<SaleBillProduct> GetSaleProductsByBillId(int billId)
        {
            return _context.SaleBillProducts.Where(p=>p.SaleBillId==billId).ToList();
        }

        // GET: api/SaleBills/code/a5bc/products
        [Route("code/{billCode}/SaleProducts")]
        [HttpGet]
        public IEnumerable<SaleBillProduct> GetSaleProductsByBillCode(string billCode)
        {
            int BillId = _context.SaleBills.Where(b => b.BillCode == billCode).Select(b => b.Id).FirstOrDefault();
            return _context.SaleBillProducts.Where(p => p.SaleBillId == BillId).Select(p => p).ToList();

        }



        private bool SaleBillExists(int id)
        {
            return _context.SaleBills.Any(e => e.Id == id);
        }

        protected bool IsSaleBillDataNotValid(SaleBill saleBill)
        {
            /*
             * ID INT IDENTITY(1,1),
              BillCode VARCHAR(50) UNIQUE,
              [Date] DATE NOT NULL Default GETDATE(),
              BillType VARCHAR(10) NOT NULL, --TODO check in cash or Debit
              Discount FLOAT NOT NULL,
              CheckNumber VARCHAR(100) NOT NULL,
              paidup FLOAT,
              Remaining FLOAT ,
              BillTotalPrice FLOAT,
              StockId INT NOT NULL,
              PayMethodId INT NOT NULL,
              ClientId INT,
              TaxId INT,
              CONSTRAINT SaleBillPK PRIMARY KEY (ID),
              CONSTRAINT SaleStockFK FOREIGN KEY (StockId) REFERENCES Stock(ID),
              CONSTRAINT SalePayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID),
              CONSTRAINT SaleClientFK FOREIGN KEY (ClientId) REFERENCES Client(ID),
              CONSTRAINT SaleTaxFK FOREIGN KEY (TaxId) REFERENCES Tax(ID),
              Constraint BillTypeCheck check(BillType in ('Cash','Debit') )
             */
            //foreign keys can not refer to Not Existed
            bool StockExisted = _context.Stocks.Any(s => s.Id == saleBill.StockId);
            bool taxExisted = _context.Taxes.Any(t => t.Id == saleBill.TaxId);
            bool PayMethodExisted = _context.PaymentMethods.Any(p => p.Id == saleBill.PayMethodId);
            bool ClientExisted = _context.Clients.Any(c => c.Id == saleBill.ClientId);

            // UNIQUE Prorerty must to be Not Existed before
            bool BillCodeRepeat = _context.SaleBills.Any((b) => b.BillCode == saleBill.BillCode && b.Id != saleBill.Id);
            bool BillTypevalid = saleBill.BillType == "Cash" || saleBill.BillType == "Debit";

            //Not NUll properties + chech Foreign and Uniqe result
            if (
                saleBill.BillCode == null || saleBill.CheckNumber == null || 
                !StockExisted || !taxExisted || !PayMethodExisted || !ClientExisted ||
                !BillTypevalid || BillCodeRepeat||
                !DateTime.TryParse(saleBill.Date.ToString(),out _)||
                saleBill.SaleBillProducts.Count==0
                //||saleBill.BillTotalPrice == 0
                )
            {
                return true;
            }
            else
            {
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
