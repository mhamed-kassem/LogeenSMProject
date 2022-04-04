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
    public class ImportPaymentsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ImportPaymentsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/ImportPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportPayment>>> GetImportPayments()
        {
            return await _context.ImportPayments.ToListAsync();
        }

        // GET: api/ImportPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportPayment>> GetImportPayment(int id)
        {
            var importPayment = await _context.ImportPayments.FindAsync(id);

            if (importPayment == null)
            {
                return NotFound();
            }

            return importPayment;
        }

        // PUT: api/ImportPayments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportPayment(int id, ImportPayment importPayment)
        {
            if (id != importPayment.Id || IsImportPaymentDataNotValid(importPayment))
            {
                return BadRequest();
            }

            _context.Entry(importPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportPaymentExists(id))
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

        // POST: api/ImportPayments
        [HttpPost]
        public async Task<ActionResult<ImportPayment>> PostImportPayment(ImportPayment importPayment)
        {
            if (IsImportPaymentDataNotValid(importPayment))
            {
                return BadRequest();
            }

            Client client = _context.Clients.Find(importPayment.ClientId);
            PaymentMethod payment = _context.PaymentMethods.Find(importPayment.PayMethodId);

            payment.Balance += importPayment.PayedBalance;
            client.BalanceOutstand -= importPayment.PayedBalance;

            _context.ImportPayments.Add(importPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImportPayment", new { id = importPayment.Id }, importPayment);
        }

        // DELETE: api/ImportPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportPayment(int id)
        {
            var importPayment = await _context.ImportPayments.FindAsync(id);
            if (importPayment == null)
            {
                return NotFound();
            }

            _context.ImportPayments.Remove(importPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportPaymentExists(int id)
        {
            return _context.ImportPayments.Any(e => e.Id == id);
        }

        protected bool IsImportPaymentDataNotValid(ImportPayment importPayment)
        {
            //--Validation 
            /*
               ID INT IDENTITY(1,1),
              [Date] DATE NOT NULL Default GETDATE(),
              PayedBalance FLOAT NOT NULL,
              CheckNumber VARCHAR(50) NOT NULL,
              SaleBillId int NOT NULL,
              ClientId int not null,
              PayMethodId int not null,
              CONSTRAINT InPayPK PRIMARY KEY (ID),
              CONSTRAINT InPayBillFK FOREIGN KEY (SaleBillId) REFERENCES SaleBill(ID),
              CONSTRAINT InPayClientFK FOREIGN KEY (ClientId) REFERENCES Client(ID),
              CONSTRAINT InPayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID)
             */

            //foreign keys can not refer to Not Existed
            bool SaleBillIdExisted = _context.SaleBills.Any(s => s.Id == importPayment.SaleBillId);
            bool ClientIdExisted = _context.Clients.Any(c => c.Id == importPayment.ClientId);
            bool PayMethodIdExisted = _context.PaymentMethods.Any(p => p.Id == importPayment.PayMethodId);

            //Not NUll properties + chech Foreign result
            if (importPayment.PayedBalance <= 0 || importPayment.CheckNumber == null ||
                !SaleBillIdExisted || !ClientIdExisted || !PayMethodIdExisted ||
                DateTime.TryParse(importPayment.Date.ToString(), out _)
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
