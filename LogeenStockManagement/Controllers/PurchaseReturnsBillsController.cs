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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseReturnsBill(int id, PurchaseReturnsBill purchaseReturnsBill)
        {
            if (id != purchaseReturnsBill.Id)
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseReturnsBill>> PostPurchaseReturnsBill(PurchaseReturnsBill purchaseReturnsBill)
        {
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
    }
}
