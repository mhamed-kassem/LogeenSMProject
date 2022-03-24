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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseBill(int id, PurchaseBill purchaseBill)
        {
            if (id != purchaseBill.Id)
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseBill>> PostPurchaseBill(PurchaseBill purchaseBill)
        {
            _context.PurchaseBills.Add(purchaseBill);
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

            _context.PurchaseBills.Remove(purchaseBill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseBillExists(int id)
        {
            return _context.PurchaseBills.Any(e => e.Id == id);
        }
    }
}
