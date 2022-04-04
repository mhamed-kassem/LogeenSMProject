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
    public class ExportPaymentsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ExportPaymentsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/ExportPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportPayment>>> GetExportPayments()
        {
            return await _context.ExportPayments.ToListAsync();
        }

        // GET: api/ExportPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportPayment>> GetExportPayment(int id)
        {
            var exportPayment = await _context.ExportPayments.FindAsync(id);

            if (exportPayment == null)
            {
                return NotFound();
            }

            return exportPayment;
        }

        // PUT: api/ExportPayments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExportPayment(int id, ExportPayment exportPayment)
        {
            if (id != exportPayment.Id || IsExportPaymentDataNotValid(exportPayment))
            {
                return BadRequest();
            }

            _context.Entry(exportPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExportPaymentExists(id))
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

        // POST: api/ExportPayments
        [HttpPost]
        public async Task<ActionResult<ExportPayment>> PostExportPayment(ExportPayment exportPayment)
        {
            if (IsExportPaymentDataNotValid(exportPayment))
            {
                return BadRequest();
            }

            Supplier supplier = _context.Suppliers.Find(exportPayment.SupplierId);
            PaymentMethod payment = _context.PaymentMethods.Find(exportPayment.PayMethodId);

            if (payment.Balance < exportPayment.PayedBalance)
            {
                return BadRequest(new
                {
                    ErrorStatus = "paymentBalance",
                    Data = payment.Name,
                    Msg = "Payment Method: " + payment.Name + " do not have enough balance."
                });
            }

            payment.Balance -= exportPayment.PayedBalance;
            supplier.BalanceDebit -= exportPayment.PayedBalance;

            _context.ExportPayments.Add(exportPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExportPayment", new { id = exportPayment.Id }, exportPayment);
        }

        // DELETE: api/ExportPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExportPayment(int id)
        {
            var exportPayment = await _context.ExportPayments.FindAsync(id);
            if (exportPayment == null)
            {
                return NotFound();
            }
            
            _context.ExportPayments.Remove(exportPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExportPaymentExists(int id)
        {
            return _context.ExportPayments.Any(e => e.Id == id);
        }

        protected bool IsExportPaymentDataNotValid(ExportPayment exportPayment)
        {
            //--Validation 
            /*
              ID INT IDENTITY(1,1),
              [Date] DATE NOT NULL Default GETDATE(),
              PayedBalance FLOAT NOT NULL,
              CheckNumber VARCHAR(50) NOT NULL,
              PurchaseBillId int NOT NULL,
              SupplierId int not null,
              PayMethodId int not null,
              CONSTRAINT OutPayPK PRIMARY KEY (ID),
              CONSTRAINT OutPayPurchaseBillFK FOREIGN KEY (PurchaseBillId) REFERENCES PurchaseBill(ID),
              CONSTRAINT OutPaySupplierFK FOREIGN KEY (SupplierId) REFERENCES Supplier(ID),
              CONSTRAINT OutPayMethodFK FOREIGN KEY (PayMethodId) REFERENCES PaymentMethod(ID)
             */

            //foreign keys can not refer to Not Existed
            bool PurchaseBillIdExisted = _context.PurchaseBills.Any(p => p.Id == exportPayment.PurchaseBillId);
            bool SupplierIdExisted = _context.Suppliers.Any(s => s.Id == exportPayment.SupplierId);
            bool PayMethodIdExisted = _context.PaymentMethods.Any(p => p.Id == exportPayment.PayMethodId);

            //Not NUll properties + chech Foreign and Uniqe result
            if (exportPayment.PayedBalance <=0||exportPayment.CheckNumber==null||
                !PurchaseBillIdExisted||!SupplierIdExisted||!PayMethodIdExisted||
                DateTime.TryParse(exportPayment.Date.ToString(),out _)
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
