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
    public class StockProductsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public StockProductsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/StockProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockProduct>>> GetStockProducts()
        {
            return await _context.StockProducts.ToListAsync();
        }

        // GET: api/StockProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockProduct>> GetStockProduct(int id)
        {
            var stockProduct = await _context.StockProducts.FindAsync(id);

            if (stockProduct == null)
            {
                return NotFound();
            }

            return stockProduct;
        }

        // PUT: api/StockProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockProduct(int id, StockProduct stockProduct)
        {
            if (id != stockProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockProductExists(id))
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

        // POST: api/StockProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockProduct>> PostStockProduct(StockProduct stockProduct)
        {
            _context.StockProducts.Add(stockProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockProduct", new { id = stockProduct.Id }, stockProduct);
        }

        // DELETE: api/StockProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockProduct(int id)
        {
            var stockProduct = await _context.StockProducts.FindAsync(id);
            if (stockProduct == null)
            {
                return NotFound();
            }

            _context.StockProducts.Remove(stockProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockProductExists(int id)
        {
            return _context.StockProducts.Any(e => e.Id == id);
        }
    }
}
