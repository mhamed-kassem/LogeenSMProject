﻿using System;
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
    public class ProductTransferedsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ProductTransferedsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/ProductTransfereds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTransfered>>> GetProductTransfereds()
        {
            return await _context.ProductTransfereds.ToListAsync();
        }

        // GET: api/ProductTransfereds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTransfered>> GetProductTransfered(int id)
        {
            var productTransfered = await _context.ProductTransfereds.FindAsync(id);

            if (productTransfered == null)
            {
                return NotFound();
            }

            return productTransfered;
        }

        // PUT: api/ProductTransfereds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductTransfered(int id, ProductTransfered productTransfered)
        {
            if (id != productTransfered.Id)
            {
                return BadRequest();
            }

            _context.Entry(productTransfered).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTransferedExists(id))
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

        // POST: api/ProductTransfereds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductTransfered>> PostProductTransfered(ProductTransfered productTransfered)
        {
            _context.ProductTransfereds.Add(productTransfered);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductTransfered", new { id = productTransfered.Id }, productTransfered);
        }

        // DELETE: api/ProductTransfereds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductTransfered(int id)
        {
            var productTransfered = await _context.ProductTransfereds.FindAsync(id);
            if (productTransfered == null)
            {
                return NotFound();
            }

            _context.ProductTransfereds.Remove(productTransfered);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductTransferedExists(int id)
        {
            return _context.ProductTransfereds.Any(e => e.Id == id);
        }
    }
}