using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public ExpenseController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("Expenses")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Expense> expenses = Context.Expenses.ToList();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        //[Route("Expenses/{id}")]
        public IActionResult GetOne(int id)
        {
            Expense expense = Context.Expenses.FirstOrDefault(s => s.Id == id);

            return Ok(expense);
        }

        //[Route("Expenses/Create")]
        [HttpPost]
        public IActionResult Post(Expense expense)
        {
            Context.Expenses.Add(expense);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("Expenses/update")]
        public void Put(int id, Expense value)
        {
            var expense = Context.Expenses.FirstOrDefault(s => s.Id == id);
            if (expense != null)
            {
                Context.Entry<Expense>(expense).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var expense = Context.Expenses.FirstOrDefault(s => s.Id == id);
            if (expense != null)
            {
                Context.Expenses.Remove(expense);
                Context.SaveChanges();
            }
        }
    }
}
