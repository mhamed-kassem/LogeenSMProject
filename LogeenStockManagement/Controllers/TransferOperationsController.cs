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
    public class TransferOperationsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public TransferOperationsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/TransferOperations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferOperation>>> GetTransferOperations()
        {
            return await _context.TransferOperations.ToListAsync();
        }

        // GET: api/TransferOperations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransferOperation>> GetTransferOperation(int id)
        {
            var transferOperation = await _context.TransferOperations.FindAsync(id);

            if (transferOperation == null)
            {
                return NotFound();
            }

            return transferOperation;
        }

        // PUT: api/TransferOperations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransferOperation(int id, TransferOperation transferOperation)
        {
            if (id != transferOperation.Id)
            {
                return BadRequest();
            }

            _context.Entry(transferOperation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransferOperationExists(id))
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

        // POST: api/TransferOperations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransferOperation>> PostTransferOperation(TransferOperation transferOperation)
        {
            _context.TransferOperations.Add(transferOperation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransferOperation", new { id = transferOperation.Id }, transferOperation);
        }

        // DELETE: api/TransferOperations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransferOperation(int id)
        {
            var transferOperation = await _context.TransferOperations.FindAsync(id);
            if (transferOperation == null)
            {
                return NotFound();
            }

            _context.TransferOperations.Remove(transferOperation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransferOperationExists(int id)
        {
            return _context.TransferOperations.Any(e => e.Id == id);
        }
    }
}
