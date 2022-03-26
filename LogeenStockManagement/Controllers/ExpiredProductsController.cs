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
    public class ExpiredProductsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ExpiredProductsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/ExpiredProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpiredProduct>>> GetExpiredProducts()
        {
            return await _context.ExpiredProducts.ToListAsync();
        }

        // GET: api/ExpiredProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpiredProduct>> GetExpiredProduct(int id)
        {
            var expiredProduct = await _context.ExpiredProducts.FindAsync(id);

            if (expiredProduct == null)
            {
                return NotFound();
            }

            return expiredProduct;
        }

        // PUT: api/ExpiredProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpiredProduct(int id, ExpiredProduct expiredProduct)
        {
            if (id != expiredProduct.Id || IsExpiredProductDataNotValid(expiredProduct))
            {
                return BadRequest();
            }

            _context.Entry(expiredProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpiredProductExists(id))
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

        // POST: api/ExpiredProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpiredProduct>> PostExpiredProduct(ExpiredProduct expiredProduct)
        {
           if (IsExpiredProductDataNotValid(expiredProduct))
            {
                return BadRequest();
            }
            
            _context.ExpiredProducts.Add(expiredProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpiredProduct", new { id = expiredProduct.Id }, expiredProduct);
        }

        // DELETE: api/ExpiredProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpiredProduct(int id)
        {
            var expiredProduct = await _context.ExpiredProducts.FindAsync(id);
            if (expiredProduct == null)
            {
                return NotFound();
            }

            

            _context.ExpiredProducts.Remove(expiredProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpiredProductExists(int id)
        {
            return _context.ExpiredProducts.Any(e => e.Id == id);
        }

        public bool IsExpiredProductDataNotValid(ExpiredProduct expiredProduct)
        {
            //--Validation 
            /*
              ID INT IDENTITY(1,1),
              Amount INT NOT NULL,
              DateAdded DATE Default GETDATE(),
              Notes VARCHAR(200),
              ProductId INT NOT NULL,
              CONSTRAINT ExpiredProductPK PRIMARY KEY (ID),
              CONSTRAINT ExpiredProductTypeFK FOREIGN KEY (ProductId) REFERENCES Product(ID)
             */

            //foreign keys can not refer to Not Existed
            bool ProductIdExisted = _context.Products.Any(p => p.Id == expiredProduct.ProductId);

            // UNIQUE Prorerty must to be Not Existed before                                               
            bool IDExisted = _context.ExpiredProducts.Any((e) => e.Id == expiredProduct.Id && e.Id != expiredProduct.Id);

            //Not NUll properties + chech Foreign and Uniqe result
            if ( //if with OR:|| if any one true do If`s body  - if(condition){body} 
                expiredProduct.Amount <= 0 ||
                expiredProduct.ProductId <=0 ||
                !ProductIdExisted || //again because it have both not null and unique 
                IDExisted
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
