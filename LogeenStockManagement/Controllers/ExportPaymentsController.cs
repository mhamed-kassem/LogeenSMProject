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
    public class ExportPaymentsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ExportPaymentsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/ExportPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportPayment>>> GetExportPayments()
        {
            return await _context.ExportPayments.ToListAsync();
        }

        // GET: api/ExportPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportPayment>> GetExportPayment(int id)
        {
            var exportPayment = await _context.ExportPayments.FindAsync(id);

            if (exportPayment == null)
            {
                return NotFound();
            }

            return exportPayment;
        }

        // PUT: api/ExportPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExportPayment(int id, ExportPayment exportPayment)
        {
            if (id != exportPayment.Id)
            {
                return BadRequest();
            }

            _context.Entry(exportPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExportPaymentExists(id))
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

        // POST: api/ExportPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExportPayment>> PostExportPayment(ExportPayment exportPayment)
        {
            _context.ExportPayments.Add(exportPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExportPayment", new { id = exportPayment.Id }, exportPayment);
        }

        // DELETE: api/ExportPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExportPayment(int id)
        {
            var exportPayment = await _context.ExportPayments.FindAsync(id);
            if (exportPayment == null)
            {
                return NotFound();
            }

            _context.ExportPayments.Remove(exportPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExportPaymentExists(int id)
        {
            return _context.ExportPayments.Any(e => e.Id == id);
        }
    }
}
