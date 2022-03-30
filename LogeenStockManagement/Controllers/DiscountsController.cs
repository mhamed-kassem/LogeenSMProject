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
    public class DiscountsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public DiscountsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Discounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
            return await _context.Discounts.ToListAsync();
        }

        // GET: api/Discounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            return discount;
        }

        // PUT: api/Discounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscount(int id, Discount discount)
        {
            if (id != discount.Id || IsDiscountDataNotValid(discount))
            {
                return BadRequest();
            }

            _context.Entry(discount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
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

        // POST: api/Discounts
        [HttpPost]
        public async Task<ActionResult<Discount>> PostDiscount(Discount discount)
        {
            if (IsDiscountDataNotValid(discount))
            {
                return BadRequest();
            }
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscount", new { id = discount.Id }, discount);
        }

        // DELETE: api/Discounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            
            if (discount == null)
            {
                return NotFound();
            }

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscountExists(int id)
        {
            return _context.Discounts.Any(e => e.Id == id);
        }

        protected bool IsDiscountDataNotValid(Discount discount)
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
            if(discount.DiscountValue<=0||discount.Notes==null||
                !DateTime.TryParse(discount.StartDate.ToString(),out _)||
                !DateTime.TryParse(discount.EndDate.ToString(),out _)||
                discount.StartDate>discount.EndDate
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
