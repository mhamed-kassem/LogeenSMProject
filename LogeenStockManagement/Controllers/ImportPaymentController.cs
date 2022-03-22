using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportPaymentController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public ImportPaymentController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("ImportPayments")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ImportPayment> importPayments = Context.ImportPayments.ToList();
            return Ok(importPayments);
        }

        [HttpGet("{id}")]
        //[Route("ImportPayments/{id}")]
        public IActionResult GetOne(decimal id)
        {
            ImportPayment importPayment = Context.ImportPayments.FirstOrDefault(s => s.Id == id);

            return Ok(importPayment);
        }

        //[Route("ImportPayments/Create")]
        [HttpPost]
        public IActionResult Post(ImportPayment importPayment)
        {
            Context.ImportPayments.Add(importPayment);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("ImportPayments/update")]
        public void Put(decimal id, ImportPayment value)
        {
            var importPayment = Context.ImportPayments.FirstOrDefault(s => s.Id == id);
            if (importPayment != null)
            {
                Context.Entry<ImportPayment>(importPayment).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(decimal id)
        {
            var importPayment = Context.ImportPayments.FirstOrDefault(s => s.Id == id);
            if (importPayment != null)
            {
                Context.ImportPayments.Remove(importPayment);
                Context.SaveChanges();
            }
        }
    }
}
