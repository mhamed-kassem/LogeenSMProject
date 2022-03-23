using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpiredProductController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public ExpiredProductController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("ExpiredProduct")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ExpiredProduct> expiredProducts = Context.ExpiredProducts.ToList();
            return Ok(expiredProducts);
        }

        [HttpGet("{id}")]
        //[Route("ExpiredProduct/{id}")]
        public IActionResult GetOne(int id)
        {
            ExpiredProduct expiredProduct = Context.ExpiredProducts.FirstOrDefault(s => s.Id == id);

            return Ok(expiredProduct);
        }

        //[Route("ExpiredProducts/Create")]
        [HttpPost]
        public IActionResult Post(ExpiredProduct expiredProduct)
        {
            Context.ExpiredProducts.Add(expiredProduct);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("ExpiredProducts/update")]
        public void Put(int id, ExpiredProduct value)
        {
            var expiredProduct = Context.ExpiredProducts.FirstOrDefault(s => s.Id == id);
            if (expiredProduct != null)
            {
                Context.Entry<ExpiredProduct>(expiredProduct).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var expiredProduct = Context.ExpiredProducts.FirstOrDefault(s => s.Id == id);
            if (expiredProduct != null)
            {
                Context.ExpiredProducts.Remove(expiredProduct);
                Context.SaveChanges();
            }
        }
    }
}
