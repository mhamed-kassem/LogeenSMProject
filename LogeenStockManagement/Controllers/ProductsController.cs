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
    public class ProductsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ProductsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            //validation section1
            if (id != product.Id || IsProductDataNotValid(product))
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            //validation section2
            if (IsProductDataNotValid(product))
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            //validation section3
            if (
                product.ExpiredProducts.Count>0||
                product.ProductTransfereds.Count>0||
                product.PurchaseProducts.Count>0||
                product.SaleBillProducts.Count>0||
                product.StockProducts.Count> 0

                )
            {
                return BadRequest();
            }


            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        public bool IsProductDataNotValid(Product product)
        {
            bool categoryExisted = _context.Products.Any(ww => ww.Id == product.CategoryId);

            bool Name = _context.Products.Any((p) => p.CategoryId == product.CategoryId && p.Id != product.Id);
            if (
                product.Name==null||
                product.Description==null||
                product.MiniAmount==0||
                product.Barcode==null||
                product.SellingPrice==0|| 
                product.PurchasingPrice==0|| 
                product.ExpiryPeriod==0|| 
                product.CategoryId==0 ||
               ! categoryExisted||
                Name

                )
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
