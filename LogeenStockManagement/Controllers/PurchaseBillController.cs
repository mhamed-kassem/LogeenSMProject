using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseBillController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public PurchaseBillController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("PurchaseBill")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<PurchaseBill> purchaseBills = Context.PurchaseBills.ToList();
            return Ok(purchaseBills);
        }

        [HttpGet("{id}")]
        //[Route("PurchaseBill/{id}")]
        public IActionResult GetOne(string id)
        {
            PurchaseBill purchaseBill = Context.PurchaseBills.FirstOrDefault(s => s.BillCode == id);

            return Ok(purchaseBill);
        }

        //[Route("PurchaseBills/Create")]
        [HttpPost]
        public IActionResult Post(PurchaseBill purchaseBill)
        {
            Context.PurchaseBills.Add(purchaseBill);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("PurchaseBills/update")]
        public void Put(string id, PurchaseBill value)
        {
            var purchaseBill = Context.PurchaseBills.FirstOrDefault(s => s.BillCode == id);
            if (purchaseBill != null)
            {
                Context.Entry<PurchaseBill>(purchaseBill).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var purchaseBill = Context.PurchaseBills.FirstOrDefault(s => s.BillCode == id);
            if (purchaseBill != null)
            {
                Context.PurchaseBills.Remove(purchaseBill);
                Context.SaveChanges();
            }
        }
    }
}
