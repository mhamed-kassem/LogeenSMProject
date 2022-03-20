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
        //[Route("stocks")]
        public IActionResult GetAll()
        {
            List<Stock> stocks = Context.Stocks.ToList();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        //[HttpGet("stocks/{id}")]
        public IActionResult GetOne(int id)
        {
            Stock stock = Context.Stocks.FirstOrDefault(s => s.Id == id);

            return Ok(stock);
        }

        [Route("stocks/Create")]
        public IActionResult Create(Stock stock)
        {
            Context.Stocks.Add(stock);
            return Ok("Created Successfully");
        }



    }
}
