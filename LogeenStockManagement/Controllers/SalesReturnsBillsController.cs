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
            if (id != salesReturnsBill.Id|| IsSaleReturnBillDataNotValid(salesReturnsBill))
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

            _context.SalesReturnsBills.Add(salesReturnsBill);
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

        private bool PurchaseReturnsBillExists(int id)
        {
            return _context.PurchaseReturnsBills.Any(e => e.Id == id);
        }

        protected bool IsSaleReturnBillDataNotValid(SalesReturnsBill SaleReturnBill)
        {
            //foreinkey
            bool SaleBillExisted = _context.SaleBills.Any(p => p.Id == SaleReturnBill.SaleBillId);
            bool TaxExisted = _context.Taxes.Any(p => p.Id == SaleReturnBill.TaxId);
            bool PayMethodExisted = _context.PaymentMethods.Any(p => p.Id == SaleReturnBill.PayMethodId);

            //uniqe
            bool CodeRepeat = _context.SalesReturnsBills.Any((p) => p.Code == SaleReturnBill.Code && p.Id != SaleReturnBill.Id);

            //Not NUll properties + chech Foreign and Uniqe result
            if (
                SaleReturnBill.Code == null ||SaleReturnBill.NetMoney <= 0 ||
                !SaleBillExisted || !TaxExisted || !PayMethodExisted ||
                CodeRepeat ||!DateTime.TryParse(SaleReturnBill.Date.ToString(), out _)
                )
            {
                return true;
            }
            else
            {
                return false;
            }

        }




    }
}
