using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypeController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public ExpenseTypeController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("ExpenseTypes")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ExpenseType> expenseTypes = Context.ExpenseTypes.ToList();
            return Ok(expenseTypes);
        }

        [HttpGet("{id}")]
        //[Route("ExpenseTypes/{id}")]
        public IActionResult GetOne(int id)
        {
            ExpenseType expenseType = Context.ExpenseTypes.FirstOrDefault(s => s.Id == id);

            return Ok(expenseType);
        }

        //[Route("ExpenseTypes/Create")]
        [HttpPost]
        public IActionResult Post(ExpenseType expenseType)
        {
            Context.ExpenseTypes.Add(expenseType);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("ExpenseTypes/update")]
        public void Put(int id, ExpenseType value)
        {
            var expenseType = Context.ExpenseTypes.FirstOrDefault(s => s.Id == id);
            if (expenseType != null)
            {
                Context.Entry<ExpenseType>(expenseType).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var expenseType = Context.ExpenseTypes.FirstOrDefault(s => s.Id == id);
            if (expenseType != null)
            {
                Context.ExpenseTypes.Remove(expenseType);
                Context.SaveChanges();
            }
        }

    }
}
