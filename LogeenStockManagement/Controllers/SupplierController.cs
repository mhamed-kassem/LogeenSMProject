using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public SupplierController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("Suppliers")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Supplier> suppliers = Context.Suppliers.ToList();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        //[Route("Suppliers/{id}")]
        public IActionResult GetOne(int id)
        {
            Supplier supplier = Context.Suppliers.FirstOrDefault(s => s.Id == id);

            return Ok(supplier);
        }

        //[Route("Suppliers/Create")]
        [HttpPost]
        public IActionResult Post(Supplier supplier)
        {
            Context.Suppliers.Add(supplier);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("Suppliers/update")]
        public void Put(int id, Supplier value)
        {
            var supplier = Context.Suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier != null)
            {
                Context.Entry<Supplier>(supplier).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var supplier = Context.Suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier != null)
            {
                Context.Suppliers.Remove(supplier);
                Context.SaveChanges();
            }
        }
    }
}
