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
    public class EmployeesController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public EmployeesController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            //validation section1
            if ( id != employee.Id || IsEmployeeDataNotValid(employee) )
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            //validation section2
            if (IsEmployeeDataNotValid(employee))
            {
                return BadRequest();
            }
            //--------------
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        public bool IsEmployeeDataNotValid(Employee employee)
        {
            //--Validation 
            /*
              ID int IDENTITY(1,1),
              [Name] VARCHAR(100) NOT NULL,
              [Address] VARCHAR(200) NOT NULL,
               NationalID NUMERIC(20) UNIQUE,
               Phone NUMERIC(20) NOT NULL UNIQUE,
              Salary FLOAT NOT NULL,
              Photo VARCHAR(100),
              Have_Access int,
               StockId INT NOT NULL,
               JobId INT NOT NULL,
              CONSTRAINT EmployeePK PRIMARY KEY (ID),
              CONSTRAINT EmployeeStockFK FOREIGN KEY (StockId) REFERENCES Stock(ID),
              CONSTRAINT EmployeeJobFK FOREIGN KEY (JobId) REFERENCES Job(ID)
             */

            //foreign keys can not refer to Not Existed
            bool StockExisted = _context.Stocks.Any(s => s.Id == employee.StockId);
            bool JobExisted = _context.Jobs.Any(j => j.Id == employee.JobId);

            // UNIQUE Prorerty must to be Not Existed before                                               
            bool NationalIDExisted = _context.Employees.Any((e) => e.NationalId == employee.NationalId && e.Id != employee.Id);
            bool PhoneExisted = _context.Employees.Any((e) => e.Phone == employee.Phone && e.Id != employee.Id);
            
            //Not NUll properties + chech Foreign and Uniqe result
            if ( //if with OR:|| if any one true do If`s body  - if(condition){body} 
                employee.Name == null ||
                employee.Address == null ||
                employee.NationalId == null || //again because it have both not null and unique 
                employee.Salary==0 ||//check null values but for numeric types
                !StockExisted || // send bad request-NotValid- when foreign key Not Existed 
                !JobExisted ||
                NationalIDExisted|| //send bad request-NotValid- when unique is Existed before
                PhoneExisted
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
