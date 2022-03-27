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
    public class CategoriesController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public CategoriesController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            //validation section1
            if (id != category.Id || IsCategoryDataNotValid(category))
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            //validation section2
            if (IsCategoryDataNotValid(category))
            {
                return BadRequest();
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            //validation section3

            if (category.Products.Count > 0) { return BadRequest(); }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        public bool IsCategoryDataNotValid(Category category)
        {
            //--Validation 
            /*
             * ID INT IDENTITY(1,1),
            [Name] VARCHAR(50) NOT NULL UNIQUE,
            [Description] VARCHAR(200) NOT NULL,
            Constraint CategoryPK PRIMARY KEY (ID)
            */

            // UNIQUE Prorerty must to be Not Existed before
            bool NameExisted = _context.Categories.Any(c => c.Name == category.Name && c.Id != category.Id);

            //Not NUll properties +  Uniqe results
            if (category.Name == null ||category.Description == null||NameExisted)
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
