using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public StockController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("stocks")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Stock> stocks = Context.Stocks.ToList();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        //[Route("stocks/{id}")]
        public IActionResult GetOne(int id)
        {
            Stock stock = Context.Stocks.FirstOrDefault(s => s.Id == id);

            return Ok(stock);
        }

        //[Route("stocks/Create")]
        [HttpPost]
        public IActionResult Post(Stock stock)
        {
            Context.Stocks.Add(stock);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("stocks/update")]
        public void Put(int id ,Stock value)
        {
            var stock = Context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock != null)
            {
                Context.Entry<Stock>(stock).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var stock = Context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock != null)
            {
                Context.Stocks.Remove(stock);
                Context.SaveChanges();
            }
        }
    }
}
