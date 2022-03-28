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
    public class DiscountForClientsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public DiscountForClientsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/DiscountForClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountForClient>>> GetDiscountForClients()
        {
            return await _context.DiscountForClients.ToListAsync();
        }

        // GET: api/DiscountForClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountForClient>> GetDiscountForClient(int id)
        {
            var discountForClient = await _context.DiscountForClients.FindAsync(id);

            if (discountForClient == null)
            {
                return NotFound();
            }

            return discountForClient;
        }

        // PUT: api/DiscountForClients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscountForClient(int id, DiscountForClient discountForClient)
        {
            if (id != discountForClient.Id || IsDiscountForClientsNotValid(discountForClient))
            {
                return BadRequest();
            }

            _context.Entry(discountForClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountForClientExists(id))
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

        // POST: api/DiscountForClients
        [HttpPost]
        public async Task<ActionResult<DiscountForClient>> PostDiscountForClient(DiscountForClient discountForClient)
        {
            if (IsDiscountForClientsNotValid(discountForClient))
            {
                return BadRequest();
            }
            _context.DiscountForClients.Add(discountForClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscountForClient", new { id = discountForClient.Id }, discountForClient);
        }

        // DELETE: api/DiscountForClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountForClient(int id)
        {
            var discountForClient = await _context.DiscountForClients.FindAsync(id);
            
            if (discountForClient == null)
            {
                return NotFound();
            }

            _context.DiscountForClients.Remove(discountForClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscountForClientExists(int id)
        {
            return _context.DiscountForClients.Any(e => e.Id == id);
        }

        protected bool IsDiscountForClientsNotValid(DiscountForClient discountForClient)
        {
            //--Validation 
              /*ID INT IDENTITY(1, 1),
              DiscountValue FLOAT NOT NULL,
              Notes VARCHAR(200),
              StartDate DATE NOT NULL,
              EndDate DATE NOT NULL,
              Constraint DiscountPK PRIMARY KEY(ID)
              */

            //Not NUll properties + chech Foreign and Uniqe results
            if(discountForClient.DiscountValue<=0||discountForClient.Notes==null||
                !DateTime.TryParse(discountForClient.StartDate.ToString(),out _)||
                !DateTime.TryParse(discountForClient.EndDate.ToString(),out _)||
                discountForClient.StartDate>discountForClient.EndDate
                )
            {
                //not valid
                return true;
            }
            else
            {
                return false;
            }



        }

        
    }
}
