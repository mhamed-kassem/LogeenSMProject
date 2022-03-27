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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraderType(int id, TraderType traderType)
        {
            if (id != traderType.Id||IsTraderTypeDataNotValid(traderType))
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
        [HttpPost]
        public async Task<ActionResult<TraderType>> PostTraderType(TraderType traderType)
        {
            if (IsTraderTypeDataNotValid(traderType))
            {
                return BadRequest();
            }

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

            if (traderType.Clients.Count > 0 || traderType.Suppliers.Count > 0)
            {
                return BadRequest();
            }

            _context.TraderTypes.Remove(traderType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraderTypeExists(int id)
        {
            return _context.TraderTypes.Any(e => e.Id == id);
        }

        public bool IsTraderTypeDataNotValid(TraderType type)
        {
            //--Validation 
            /*
              ID INT IDENTITY(1,1),
              TypeName VARCHAR(50) NOT NULL UNIQUE,
              [Description] VARCHAR(200),
              Constraint TraderTypePK PRIMARY KEY (ID)
             */

            // UNIQUE Prorerty must to be Not Existed before
            bool TypeNameRepeat = _context.TraderTypes.Any((tt) => tt.TypeName == type.TypeName && tt.Id != type.Id);

            //Not NUll properties + chech Uniqe result
            if (type.TypeName == null || TypeNameRepeat)
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
