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
    public class ImportPaymentsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ImportPaymentsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/ImportPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportPayment>>> GetImportPayments()
        {
            return await _context.ImportPayments.ToListAsync();
        }

        // GET: api/ImportPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportPayment>> GetImportPayment(int id)
        {
            var importPayment = await _context.ImportPayments.FindAsync(id);

            if (importPayment == null)
            {
                return NotFound();
            }

            return importPayment;
        }

        // PUT: api/ImportPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportPayment(int id, ImportPayment importPayment)
        {
            if (id != importPayment.Id)
            {
                return BadRequest();
            }

            _context.Entry(importPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportPaymentExists(id))
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

        // POST: api/ImportPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImportPayment>> PostImportPayment(ImportPayment importPayment)
        {
            _context.ImportPayments.Add(importPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImportPayment", new { id = importPayment.Id }, importPayment);
        }

        // DELETE: api/ImportPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportPayment(int id)
        {
            var importPayment = await _context.ImportPayments.FindAsync(id);
            if (importPayment == null)
            {
                return NotFound();
            }

            _context.ImportPayments.Remove(importPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportPaymentExists(int id)
        {
            return _context.ImportPayments.Any(e => e.Id == id);
        }
    }
}
