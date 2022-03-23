using Microsoft.AspNetCore.Mvc;
using LogeenStockManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogeenJobManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        LogeenStockManagementContext Context;
        public JobController(LogeenStockManagementContext context)
        {
            Context = context;
        }
        //[route("Jobs")]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Job> jobs = Context.Jobs.ToList();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        //[Route("Jobs/{id}")]
        public IActionResult GetOne(int id)
        {
            Job job = Context.Jobs.FirstOrDefault(s => s.Id == id);

            return Ok(job);
        }

        //[Route("Jobs/Create")]
        [HttpPost]
        public IActionResult Post(Job job)
        {
            Context.Jobs.Add(job);
            Context.SaveChanges();
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        //[Route("Jobs/update")]
        public void Put(int id, Job value)
        {
            var job = Context.Jobs.FirstOrDefault(s => s.Id == id);
            if (job != null)
            {
                Context.Entry<Job>(job).CurrentValues.SetValues(value);
                Context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var job = Context.Jobs.FirstOrDefault(s => s.Id == id);
            if (job != null)
            {
                Context.Jobs.Remove(job);
                Context.SaveChanges();
            }
        }
    }
}
