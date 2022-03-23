using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public PaymentMethodController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("PaymentMethod")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<PaymentMethod> paymentMethods = Context.PaymentMethods.ToList();
            return Ok(paymentMethods);
        }

        [HttpGet("{id}")]
        //[Route("PaymentMethod/{id}")]
        public IActionResult GetOne(int id)
        {
            PaymentMethod paymentMethod = Context.PaymentMethods.FirstOrDefault(s => s.Id == id);

            return Ok(paymentMethod);
        }

        //[Route("PaymentMethods/Create")]
        [HttpPost]
        public IActionResult Post(PaymentMethod paymentMethod)
        {
            Context.PaymentMethods.Add(paymentMethod);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("PaymentMethods/update")]
        public void Put(int id, PaymentMethod value)
        {
            var paymentMethod = Context.PaymentMethods.FirstOrDefault(s => s.Id == id);
            if (paymentMethod != null)
            {
                Context.Entry<PaymentMethod>(paymentMethod).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var paymentMethod = Context.PaymentMethods.FirstOrDefault(s => s.Id == id);
            if (paymentMethod != null)
            {
                Context.PaymentMethods.Remove(paymentMethod);
                Context.SaveChanges();
            }
        }
    }
}
