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
    public class TransferOperationsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public TransferOperationsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/TransferOperations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferOperation>>> GetTransferOperations()
        {
            return await _context.TransferOperations.ToListAsync();
        }

        // GET: api/TransferOperations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransferOperation>> GetTransferOperation(int id)
        {
            var transferOperation = await _context.TransferOperations.FindAsync(id);

            if (transferOperation == null)
            {
                return NotFound();
            }

            return transferOperation;
        }

        // PUT: api/TransferOperations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransferOperation(int id, TransferOperation transferOperation)
        {
            if (id != transferOperation.Id||IsTransferOP_DataNotValid(transferOperation))
            {
                return BadRequest();
            }

            _context.Entry(transferOperation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransferOperationExists(id))
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

        // POST: api/TransferOperations
        [HttpPost]
        public async Task<ActionResult<TransferOperation>> PostTransferOperation(TransferOperation transferOperation)
        {
            if (IsTransferOP_DataNotValid(transferOperation))
            {
                return BadRequest();
            }

            _context.TransferOperations.Add(transferOperation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransferOperation", new { id = transferOperation.Id }, transferOperation);
        }

        // DELETE: api/TransferOperations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransferOperation(int id)
        {
            var transferOperation = await _context.TransferOperations.FindAsync(id);
            if (transferOperation == null)
            {
                return NotFound();
            }
            if(transferOperation.ProductTransfereds.Count > 0){ return BadRequest(); }
            
            _context.TransferOperations.Remove(transferOperation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransferOperationExists(int id)
        {
            return _context.TransferOperations.Any(e => e.Id == id);
        }

        protected bool IsTransferOP_DataNotValid(TransferOperation operation)
        {
            //--Validation 
            /*
            ID INT IDENTITY(1,1),
            [Date] DATE NOT NULL  Default GETDATE(), --TODO  defalt value today
            Notes VARCHAR(200),
            EmployeeId int not null,
            FromStockId INT NOT NULL,
            ToStockId INT NOT NULL,
            Constraint TransferPK PRIMARY KEY (ID),
            Constraint TransferEmployeeFK FOREIGN KEY (EmployeeId) REFERENCES Employee(ID), 
            Constraint TransferFromStockFK FOREIGN KEY (FromStockId) REFERENCES Stock(ID),
            Constraint TransferToStockFK FOREIGN KEY (ToStockId) REFERENCES Stock(ID) 
            */

            //foreign keys can not refer to Not Existed
            bool EmployeeExisted = _context.Employees.Any(e => e.Id == operation.EmployeeId);
            bool FromStockExisted = _context.Stocks.Any(s => s.Id == operation.FromStockId);
            bool ToStockExisted = _context.Stocks.Any(s => s.Id == operation.ToStockId);

            //Not NUll properties + chech Foreign and Uniqe results
            if (DateTime.TryParse(operation.Date.ToString(),out _)||
                !EmployeeExisted||!FromStockExisted||!ToStockExisted)
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
