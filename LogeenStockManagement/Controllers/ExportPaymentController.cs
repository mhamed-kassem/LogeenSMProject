using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportPaymentController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public ExportPaymentController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("ExportPayment")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ExportPayment> exportPayments = Context.ExportPayments.ToList();
            return Ok(exportPayments);
        }

        [HttpGet("{id}")]
        //[Route("ExportPayment/{id}")]
        public IActionResult GetOne(int id)
        {
            ExportPayment exportPayment = Context.ExportPayments.FirstOrDefault(s => s.Id == id);

            return Ok(exportPayment);
        }

        //[Route("ExportPayments/Create")]
        [HttpPost]
        public IActionResult Post(ExportPayment exportPayment)
        {
            Context.ExportPayments.Add(exportPayment);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("ExportPayments/update")]
        public void Put(int id, ExportPayment value)
        {
            var exportPayment = Context.ExportPayments.FirstOrDefault(s => s.Id == id);
            if (exportPayment != null)
            {
                Context.Entry<ExportPayment>(exportPayment).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var exportPayment = Context.ExportPayments.FirstOrDefault(s => s.Id == id);
            if (exportPayment != null)
            {
                Context.ExportPayments.Remove(exportPayment);
                Context.SaveChanges();
            }
        }
    }
}
