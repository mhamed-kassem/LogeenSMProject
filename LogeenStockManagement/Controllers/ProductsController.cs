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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id|| IsProductDataNotValid(product))
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
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
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

            if(product.ExpiredProducts.Count>0||
                product.ProductTransfereds.Count>0||
                product.PurchaseProducts.Count>0||
                product.SaleBillProducts.Count>0||
                product.StockProducts.Count>0
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

        protected bool IsProductDataNotValid(Product product)
        {
            //--Validation 
            /*
            ID INT IDENTITY(1,1),
            [Name] VARCHAR(50) NOT NULL UNIQUE,
            [Description] VARCHAR(200),
            MiniAmount INT NOT NULL,
            Barcode VARCHAR(100),
            SellingPrice FLOAT NOT NULL,
            PurchasingPrice FLOAT NOT NULL,
            ExpiryPeriod INT NOT NULL,
            CategoryId INT NOT NULL,
            CONSTRAINT ProductPK PRIMARY KEY (ID),
            CONSTRAINT ProductCategoryFK FOREIGN KEY (CategoryId) REFERENCES Category(ID)
            --UNIQUE (Barcode) --TODO
             */

            //foreign keys can not refer to Not Existed
            bool CategoryExisted = _context.Categories.Any(c => c.Id == product.CategoryId);
            
            // UNIQUE Prorerty must to be Not Existed before                                               
            bool NameExisted = _context.Products.Any((p) => p.Name == product.Name && p.Id != product.Id);
            bool BarcodeExisted= _context.Products.Any((p) => p.Barcode == product.Barcode && p.Id != product.Id);


            //Not NUll properties + chech Foreign and Uniqe result
            if (product.Name == null ||product.MiniAmount<=0||product.SellingPrice<=0||product.PurchasingPrice<=0||product.ExpiryPeriod<=0||
                NameExisted||BarcodeExisted||
                !CategoryExisted
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
