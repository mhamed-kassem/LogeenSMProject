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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleBill(int id, SaleBill saleBill)
        {
            if (id != saleBill.Id)
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaleBill>> PostSaleBill(SaleBill saleBill)
        {
            _context.SaleBills.Add(saleBill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleBill", new { id = saleBill.Id }, saleBill);
        }

        // DELETE: api/SaleBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleBill(int id)
        {
            var saleBill = await _context.SaleBills.FindAsync(id);
            if (saleBill == null)
            {
                return NotFound();
            }

            _context.SaleBills.Remove(saleBill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleBillExists(int id)
        {
            return _context.SaleBills.Any(e => e.Id == id);
        }
    }
}
