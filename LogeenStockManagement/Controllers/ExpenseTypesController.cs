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
    public class ExpenseTypesController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ExpenseTypesController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseType>>> GetExpenseTypes()
        {
            return await _context.ExpenseTypes.ToListAsync();
        }

        // GET: api/ExpenseTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseType>> GetExpenseType(int id)
        {
            var expenseType = await _context.ExpenseTypes.FindAsync(id);

            if (expenseType == null)
            {
                return NotFound();
            }

            return expenseType;
        }

        // PUT: api/ExpenseTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseType(int id, ExpenseType expenseType)
        {
            if (id != expenseType.Id|| IsExpenseTypeNotValid(expenseType))
            {
                return BadRequest();
            }

            _context.Entry(expenseType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseTypeExists(id))
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

        // POST: api/ExpenseTypes
        [HttpPost]
        public async Task<ActionResult<ExpenseType>> PostExpenseType(ExpenseType expenseType)
        {
            if (IsExpenseTypeNotValid(expenseType))
            {
                return BadRequest();
            }

            _context.ExpenseTypes.Add(expenseType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenseType", new { id = expenseType.Id }, expenseType);
        }

        // DELETE: api/ExpenseTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseType(int id)
        {
            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }

            if (expenseType.Expenses.Count > 0)
            {
                return BadRequest();
            }

            _context.ExpenseTypes.Remove(expenseType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseTypeExists(int id)
        {
            return _context.ExpenseTypes.Any(e => e.Id == id);
        }

        public bool IsExpenseTypeNotValid(ExpenseType expenseType)
        {
            //--Validation 
            /*
            ID INT IDENTITY(1,1),
            [Name] VARCHAR(50) NOT NULL,
            Details VARCHAR(150) NOT NULL,
            Constraint ExpenseTypePK PRIMARY KEY (ID)
             */

            //Not NUll properties + chech Foreign and Uniqe result
            if (expenseType.Name == null || expenseType.Details == null||
                _context.ExpenseTypes.Any((t) => t.Name == expenseType.Name && t.Id != expenseType.Id)
                )
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
