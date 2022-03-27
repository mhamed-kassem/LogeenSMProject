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
    public class SaleBillProductsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public SaleBillProductsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/SaleBillProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleBillProduct>>> GetSaleBillProducts()
        {
            return await _context.SaleBillProducts.ToListAsync();
        }

        // GET: api/SaleBillProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleBillProduct>> GetSaleBillProduct(int id)
        {
            var saleBillProduct = await _context.SaleBillProducts.FindAsync(id);

            if (saleBillProduct == null)
            {
                return NotFound();
            }

            return saleBillProduct;
        }

        // PUT: api/SaleBillProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleBillProduct(int id, SaleBillProduct saleBillProduct)
        {
            //validation section1
            if (id != saleBillProduct.Id || IsSaleBillProductDataNotValid(saleBillProduct))
            {
                return BadRequest();
            }

            _context.Entry(saleBillProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleBillProductExists(id))
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

        // POST: api/SaleBillProducts
        [HttpPost]
        public async Task<ActionResult<SaleBillProduct>> PostSaleBillProduct(SaleBillProduct saleBillProduct)
        {
            //validation section2
            if (IsSaleBillProductDataNotValid(saleBillProduct))
            {
                return BadRequest();
            }
            _context.SaleBillProducts.Add(saleBillProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleBillProduct", new { id = saleBillProduct.Id }, saleBillProduct);
        }

        // DELETE: api/SaleBillProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleBillProduct(int id)
        {
            var saleBillProduct = await _context.SaleBillProducts.FindAsync(id);
            if (saleBillProduct == null)
            {
                return NotFound();
            }

            _context.SaleBillProducts.Remove(saleBillProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleBillProductExists(int id)
        {
            return _context.SaleBillProducts.Any(e => e.Id == id);
        }
        public bool IsSaleBillProductDataNotValid(SaleBillProduct saleBillProduct)
        {
            //foreinkey
            bool SaleBillExisted = _context.SaleBillProducts.Any(s => s.Id == saleBillProduct.SaleBillId);
            bool ProductTypeExisted = _context.SaleBillProducts.Any(s => s.Id == saleBillProduct.ProductId);
            
            //Not NUll properties + chech Foreign and Uniqe result
            if (
                saleBillProduct.AmountToSell==0||
                saleBillProduct.TotalPrice==0||
                !SaleBillExisted||!ProductTypeExisted
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
