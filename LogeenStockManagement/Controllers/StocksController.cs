using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogeenStockManagement.Models;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public StocksController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            return await _context.Stocks.ToListAsync();
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        // PUT: api/Stocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(int id, Stock stock)
        {
            if (id != stock.Id||IsStockDataNotValid(stock))
            {
                return BadRequest();
            }

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Stocks
        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            if (IsStockDataNotValid(stock))
            {
                return BadRequest();
            }

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock", new { id = stock.Id }, stock);
        }

        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            if (stock.Employees.Count > 0 || stock.Expenses.Count > 0 ||
                stock.PurchaseBills.Count > 0 || stock.SaleBills.Count > 0 ||
                stock.StockProducts.Count > 0 ||
                stock.TransferOperationFromStocks.Count > 0 || stock.TransferOperationToStocks.Count > 0)
            {
                return BadRequest();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.Id == id);
        }

        public bool IsStockDataNotValid(Stock stock)
        {
            /*
             * ID INT IDENTITY(1,1),
	        [Name] VARCHAR(50) NOT NULL UNIQUE,
	        [Address] VARCHAR(200) NOT NULL UNIQUE,
	        Constraint StockPK PRIMARY KEY (ID)           
             */

            // UNIQUE Prorerty must to be Not Existed before
            bool NameRepeat = _context.Stocks.Any((s) => s.Name == stock.Name && s.Id != s.Id);
            bool AddressRepeat = _context.Stocks.Any((s) => s.Address == stock.Address && s.Id != s.Id);

            //Not NUll properties + chech Foreign and Uniqe result
            if (stock.Name == null || stock.Address == null ||
                NameRepeat || AddressRepeat)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
