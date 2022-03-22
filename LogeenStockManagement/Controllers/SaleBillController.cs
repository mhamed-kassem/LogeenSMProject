using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleBillController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public SaleBillController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("SaleBill")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<SaleBill> saleBills = Context.SaleBills.ToList();
            return Ok(saleBills);
        }

        [HttpGet("{id}")]
        //[Route("SaleBill/{id}")]
        public IActionResult GetOne(string id)
        {
            SaleBill saleBill = Context.SaleBills.FirstOrDefault(s => s.BillCode == id);

            return Ok(saleBill);
        }

        //[Route("SaleBills/Create")]
        [HttpPost]
        public IActionResult Post(SaleBill saleBill)
        {
            Context.SaleBills.Add(saleBill);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("SaleBills/update")]
        public void Put(string id, SaleBill value)
        {
            var saleBill = Context.SaleBills.FirstOrDefault(s => s.BillCode == id);
            if (saleBill != null)
            {
                Context.Entry<SaleBill>(saleBill).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var SaleBill = Context.SaleBills.FirstOrDefault(s => s.BillCode == id);
            if (SaleBill != null)
            {
                Context.SaleBills.Remove(SaleBill);
                Context.SaveChanges();
            }
        }
    }
}
