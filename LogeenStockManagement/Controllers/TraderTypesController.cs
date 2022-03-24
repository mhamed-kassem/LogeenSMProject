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
    public class TraderTypesController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public TraderTypesController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/TraderTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraderType>>> GetTraderTypes()
        {
            return await _context.TraderTypes.ToListAsync();
        }

        // GET: api/TraderTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraderType>> GetTraderType(int id)
        {
            var traderType = await _context.TraderTypes.FindAsync(id);

            if (traderType == null)
            {
                return NotFound();
            }

            return traderType;
        }

        // PUT: api/TraderTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraderType(int id, TraderType traderType)
        {
            if (id != traderType.Id)
            {
                return BadRequest();
            }

            _context.Entry(traderType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraderTypeExists(id))
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

        // POST: api/TraderTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraderType>> PostTraderType(TraderType traderType)
        {
            _context.TraderTypes.Add(traderType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTraderType", new { id = traderType.Id }, traderType);
        }

        // DELETE: api/TraderTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraderType(int id)
        {
            var traderType = await _context.TraderTypes.FindAsync(id);
            if (traderType == null)
            {
                return NotFound();
            }

            _context.TraderTypes.Remove(traderType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraderTypeExists(int id)
        {
            return _context.TraderTypes.Any(e => e.Id == id);
        }
    }
}
