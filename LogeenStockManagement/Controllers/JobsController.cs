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
    public class JobsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public JobsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return await _context.Jobs.ToListAsync();
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.Id || IsjobDataNotValid(job))
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            if (IsjobDataNotValid(job)) { return BadRequest(); }
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            //validation section3
            if (job.Employees.Count > 0 ) { return BadRequest(); }
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }

        public bool IsjobDataNotValid(Job job)
        {
            //--Validation 
            /*
              ID INT IDENTITY(1,1),
              JobTitle VARCHAR(50) NOT NULL UNIQUE,
              JobDescription VARCHAR(200),
              Constraint JobPK PRIMARY KEY (ID)
             */

            // UNIQUE Prorerty must to be Not Existed before                                               
            bool IDExisted = _context.Jobs.Any((j) => j.Id == job.Id && j.Id != job.Id);
            bool JobTitleExisted = _context.Jobs.Any((j) => j.JobTitle == job.JobTitle && j.JobTitle != job.JobTitle);

            //Not NUll properties + chech Foreign and Uniqe result
            if ( //if with OR:|| if any one true do If`s body  - if(condition){body} 
                job.Id <= 0 ||
                job.JobTitle == null ||
                IDExisted || //again because it have both not null and unique 
                JobTitleExisted//check null values but for numeric types
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
