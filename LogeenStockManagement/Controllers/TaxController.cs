using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public TaxController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("Tax")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Tax> taxs = Context.Taxes.ToList();
            return Ok(taxs);
        }

        [HttpGet("{id}")]
        //[Route("Tax/{id}")]
        public IActionResult GetOne(int id)
        {
            Tax tax = Context.Taxes.FirstOrDefault(s => s.Id == id);

            return Ok(tax);
        }

        //[Route("Taxs/Create")]
        [HttpPost]
        public IActionResult Post(Tax tax)
        {
            Context.Taxes.Add(tax);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("Taxs/update")]
        public void Put(int id, Tax value)
        {
            var tax = Context.Taxes.FirstOrDefault(s => s.Id == id);
            if (tax != null)
            {
                Context.Entry<Tax>(tax).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tax = Context.Taxes.FirstOrDefault(s => s.Id == id);
            if (tax != null)
            {
                Context.Taxes.Remove(tax);
                Context.SaveChanges();
            }
        }
    }
}
