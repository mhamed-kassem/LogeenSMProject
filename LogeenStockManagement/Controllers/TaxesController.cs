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
    public class TaxesController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public TaxesController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Taxes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tax>>> GetTaxes()
        {
            return await _context.Taxes.ToListAsync();
        }

        // GET: api/Taxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tax>> GetTax(int id)
        {
            var tax = await _context.Taxes.FindAsync(id);

            if (tax == null)
            {
                return NotFound();
            }

            return tax;
        }

        // PUT: api/Taxes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTax(int id, Tax tax)
        {
            if (id != tax.Id||IsTaxDataNotValid(tax))
            {
                return BadRequest();
            }

            _context.Entry(tax).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxExists(id))
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

        // POST: api/Taxes
        [HttpPost]
        public async Task<ActionResult<Tax>> PostTax(Tax tax)
        {
            if (IsTaxDataNotValid(tax))
            {
                return BadRequest();
            }

            _context.Taxes.Add(tax);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTax", new { id = tax.Id }, tax);
        }

        // DELETE: api/Taxes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTax(int id)
        {
            var tax = await _context.Taxes.FindAsync(id);
            if (tax == null)
            {
                return NotFound();
            }
            if(tax.PurchaseBills.Count>0||tax.PurchaseReturnsBills.Count>0||
                tax.SaleBills.Count > 0 || tax.SalesReturnsBills.Count > 0)
            {
                return BadRequest();
            }

            _context.Taxes.Remove(tax);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaxExists(int id)
        {
            return _context.Taxes.Any(e => e.Id == id);
        }

        public bool IsTaxDataNotValid(Tax tax)
        {
            //--Validation 
            /*
            ID INT IDENTITY(1,1),
            [Name] VARCHAR(50) NOT NULL UNIQUE,
            [Percentage] INT NOT NULL,
            [Description] VARCHAR(200) NOT NULL,
            Constraint TaxPK PRIMARY KEY (ID)
            */

            // UNIQUE Prorerty must to be Not Existed before
            bool NameRepeat = _context.Taxes.Any(t => t.Name == tax.Name && t.Id != tax.Id);

            //Not NUll properties + chech Foreign and Uniqe results
            if (tax.Name == null || tax.Percentage <= 0 || tax.Description == null ||
                NameRepeat)
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
