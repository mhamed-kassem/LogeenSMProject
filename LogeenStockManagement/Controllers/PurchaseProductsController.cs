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
    public class PurchaseProductsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public PurchaseProductsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseProduct>>> GetPurchaseProducts()
        {
            return await _context.PurchaseProducts.ToListAsync();
        }

        // GET: api/PurchaseProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseProduct>> GetPurchaseProduct(int id)
        {
            var purchaseProduct = await _context.PurchaseProducts.FindAsync(id);

            if (purchaseProduct == null)
            {
                return NotFound();
            }

            return purchaseProduct;
        }

        // PUT: api/PurchaseProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseProduct(int id, PurchaseProduct purchaseProduct)
        {
            if (id != purchaseProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaseProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseProductExists(id))
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

        // POST: api/PurchaseProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseProduct>> PostPurchaseProduct(PurchaseProduct purchaseProduct)
        {
            _context.PurchaseProducts.Add(purchaseProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseProduct", new { id = purchaseProduct.Id }, purchaseProduct);
        }

        // DELETE: api/PurchaseProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseProduct(int id)
        {
            var purchaseProduct = await _context.PurchaseProducts.FindAsync(id);
            if (purchaseProduct == null)
            {
                return NotFound();
            }

            _context.PurchaseProducts.Remove(purchaseProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseProductExists(int id)
        {
            return _context.PurchaseProducts.Any(e => e.Id == id);
        }
    }
}
