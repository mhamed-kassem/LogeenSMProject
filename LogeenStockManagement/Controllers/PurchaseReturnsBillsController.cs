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
    public class PurchaseReturnsBillsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public PurchaseReturnsBillsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseReturnsBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseReturnsBill>>> GetPurchaseReturnsBills()
        {
            return await _context.PurchaseReturnsBills.ToListAsync();
        }

        // GET: api/PurchaseReturnsBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseReturnsBill>> GetPurchaseReturnsBill(int id)
        {
            var purchaseReturnsBill = await _context.PurchaseReturnsBills.FindAsync(id);

            if (purchaseReturnsBill == null)
            {
                return NotFound();
            }

            return purchaseReturnsBill;
        }

        // PUT: api/PurchaseReturnsBills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseReturnsBill(int id, PurchaseReturnsBill purchaseReturnsBill)
        {
            //validation section1
            if (id != purchaseReturnsBill.Id || IsPurchaseReturnsBillDataNotValid(purchaseReturnsBill))
            {
                return BadRequest();
            }

            _context.Entry(purchaseReturnsBill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseReturnsBillExists(id))
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

        // POST: api/PurchaseReturnsBills
        [HttpPost]
        public async Task<ActionResult<PurchaseReturnsBill>> PostPurchaseReturnsBill(PurchaseReturnsBill purchaseReturnsBill)
        {
            //validation section2
            if (IsPurchaseReturnsBillDataNotValid(purchaseReturnsBill))
            {
                return BadRequest();

            }


            _context.PurchaseReturnsBills.Add(purchaseReturnsBill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseReturnsBill", new { id = purchaseReturnsBill.Id }, purchaseReturnsBill);
        }

        // DELETE: api/PurchaseReturnsBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseReturnsBill(int id)
        {
            var purchaseReturnsBill = await _context.PurchaseReturnsBills.FindAsync(id);
            if (purchaseReturnsBill == null)
            {
                return NotFound();
            }

            _context.PurchaseReturnsBills.Remove(purchaseReturnsBill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseReturnsBillExists(int id)
        {
            return _context.PurchaseReturnsBills.Any(e => e.Id == id);
        }

        protected bool IsPurchaseReturnsBillDataNotValid(PurchaseReturnsBill purchaseReturnsBill)
        {
            //foreinkey
            bool PurchaseBillExisted = _context.PurchaseBills.Any(p => p.Id == purchaseReturnsBill.PurchaseBillId);
            bool TaxExisted = _context.Taxes.Any(p => p.Id == purchaseReturnsBill.TaxId);
            bool PayMethodExisted = _context.PaymentMethods.Any(p => p.Id == purchaseReturnsBill.PayMethodId);
            
            //uniqe
            bool CodeRepeat = _context.PurchaseReturnsBills.Any((p) => p.Code == purchaseReturnsBill.Code && p.Id != purchaseReturnsBill.Id);
            
            //Not NUll properties + chech Foreign and Uniqe result
            if (
                purchaseReturnsBill.Code==null||purchaseReturnsBill.NetMoney==0
                ||!PurchaseBillExisted||!TaxExisted||!PayMethodExisted||CodeRepeat
                ||!DateTime.TryParse(purchaseReturnsBill.Date.ToString(), out _)
                ||purchaseReturnsBill.ReturnPurchaseProducts.Count<=0
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
 