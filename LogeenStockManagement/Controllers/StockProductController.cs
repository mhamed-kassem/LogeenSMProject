using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockProductController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public StockProductController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("StockProducts")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<StockProduct> stockProducts = Context.StockProducts.ToList();
            return Ok(stockProducts);
        }

        [HttpGet("{id}")]
        //[Route("StockProducts/{id}")]
        public IActionResult GetOne(int id)
        {
            StockProduct stockProduct = Context.StockProducts.FirstOrDefault(s => s.StockId == id);

            return Ok(stockProduct);
        }

        //[Route("StockProducts/Create")]
        [HttpPost]
        public IActionResult Post(StockProduct stockProduct)
        {
            Context.StockProducts.Add(stockProduct);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("StockProducts/update")]
        public void Put(int id, StockProduct value)
        {
            var stockProduct = Context.StockProducts.FirstOrDefault(s => s.StockId == id);
            if (stockProduct != null)
            {
                Context.Entry<StockProduct>(stockProduct).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var stockProduct = Context.StockProducts.FirstOrDefault(s => s.StockId == id);
            if (stockProduct != null)
            {
                Context.StockProducts.Remove(stockProduct);
                Context.SaveChanges();
            }
        }
    }
}
