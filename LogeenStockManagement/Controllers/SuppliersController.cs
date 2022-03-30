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
    public class SuppliersController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public SuppliersController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        // PUT: api/Suppliers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(int id, Supplier supplier)
        {
            if (id != supplier.Id||IsSupplierDataNotValid(supplier))
            {
                return BadRequest();
            }

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        // POST: api/Suppliers
        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            if (IsSupplierDataNotValid(supplier))
            {
                return BadRequest();
            }

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplier", new { id = supplier.Id }, supplier);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            if (supplier.BalanceDebit > 0 || supplier.ExportPayments.Count > 0 || supplier.PurchaseBills.Count > 0)
            {
                return BadRequest();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }

        protected bool IsSupplierDataNotValid(Supplier supplier)
        {
            //--Validation 
            /*
            ID INT IDENTITY(1,1),
            [Name] VARCHAR(50) NOT NULL UNIQUE,
            Phone NUMERIC(20) NOT NULL UNIQUE,
            [Address] VARCHAR(100) NOT NULL,
            BalanceDebit FLOAT,
            TypeId INT NOT NULL,
            CONSTRAINT SupplierPK PRIMARY KEY (ID),
            CONSTRAINT SupplierTypeFK FOREIGN KEY (TypeId) REFERENCES TraderType(ID)
            */

            //foreign keys can not refer to Not Existed
            bool TypeIdExisted = _context.TraderTypes.Any(t => t.Id == supplier.TypeId);

            // UNIQUE Prorerty must to be Not Existed before
            bool NameRepeat= _context.Suppliers.Any(s => s.Name == supplier.Name && s.Id != supplier.Id);
            bool PhoneRepeat = _context.Suppliers.Any(s => s.Phone == supplier.Phone && s.Id != supplier.Id);

            //Not NUll properties + chech Foreign and Uniqe results
            if (supplier.Name == null || supplier.Phone == null ||supplier.Address==null||
                PhoneRepeat || NameRepeat || !TypeIdExisted)
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
