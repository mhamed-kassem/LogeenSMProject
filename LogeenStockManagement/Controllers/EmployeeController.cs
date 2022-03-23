using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public EmployeeController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("Employees")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Employee> employees = Context.Employees.ToList();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        //[Route("Employees/{id}")]
        public IActionResult GetOne(decimal id)
        {
            Employee employee = Context.Employees.FirstOrDefault(s => s.NationalId == id);

            return Ok(employee);
        }

        //[Route("Employees/Create")]
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            Context.Employees.Add(employee);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("Employees/update")]
        public void Put(decimal id, Employee value)
        {
            var employee = Context.Employees.FirstOrDefault(s => s.NationalId == id);
            if (employee != null)
            {
                Context.Entry<Employee>(employee).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(decimal id)
        {
            var employee = Context.Employees.FirstOrDefault(s => s.NationalId == id);
            if (employee != null)
            {
                Context.Employees.Remove(employee);
                Context.SaveChanges();
            }
        }
    }
}
