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
    public class ExpensesController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ExpensesController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id || IsExpenseDataNotValid(expense))
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            //validation section2
            if (IsExpenseDataNotValid(expense))
            {
                return BadRequest();
            }
            //--------------

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }

        public bool IsExpenseDataNotValid(Expense expense)
        {
            //--Validation 
            /*
              ID INT IDENTITY(1,1),
              [Date] DATE NOT NULL Default GETDATE(), --TODO default today
              Notes VARCHAR(200),
              [Value] FLOAT NOT NULL,
              CheckNumber VARCHAR(50) NOT NULL,
              StockId INT NOT NULL,
              EmployeeId int,
              TypeId INT NOT NULL,
              PayMethodId INT NOT NULL,
              CONSTRAINT ExpensePK PRIMARY KEY (ID),
              CONSTRAINT ExpenseStockFK FOREIGN KEY (StockId) REFERENCES Stock(ID),
              CONSTRAINT ExpenseEmployeFK FOREIGN KEY (EmployeeId) REFERENCES Employee(ID),
              CONSTRAINT ExpenseTypeFK FOREIGN KEY (TypeId) REFERENCES ExpenseType(ID),
              CONSTRAINT ExpensePayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID)
             */

            //foreign keys can not refer to Not Existed
            bool StockExisted = _context.Stocks.Any(s => s.Id == expense.StockId);
            bool EmployeeExisted = _context.Employees.Any(e => e.Id == expense.EmployeeId);
            bool ExpenseTypeExisted= _context.ExpenseTypes.Any(e => e.Id == expense.TypeId);
            bool PaymentMethodExisted = _context.PaymentMethods.Any(p => p.Id == expense.PayMethodId);


            // UNIQUE Prorerty must to be Not Existed before                                               
            bool IDExisted = _context.Expenses.Any((e) => e.Id == expense.Id && e.Id != expense.Id);

            //Not NUll properties + chech Foreign and Uniqe result
            if ( //if with OR:|| if any one true do If`s body  - if(condition){body} 
                expense.Id == 0 ||
                //expense.Date==0||
                expense.Value == 0 ||
                expense.CheckNumber == null || //again because it have both not null and unique 
                IDExisted ||//check null values but for numeric types
                !StockExisted || // send bad request-NotValid- when foreign key Not Existed 
                !EmployeeExisted ||
                !ExpenseTypeExisted || //send bad request-NotValid- when unique is Existed before
                !PaymentMethodExisted ||
                IDExisted
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
