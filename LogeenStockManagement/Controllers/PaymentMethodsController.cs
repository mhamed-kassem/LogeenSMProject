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
    public class PaymentMethodsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public PaymentMethodsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(int id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return paymentMethod;
        }

        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id || IsPaymentMethodDataNotValid(paymentMethod))
            {
                return BadRequest();
            }

            _context.Entry(paymentMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodExists(id))
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

        // POST: api/PaymentMethods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            if (IsPaymentMethodDataNotValid(paymentMethod)) { return BadRequest(); }
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            if (paymentMethod.Expenses.Count>0 || 
                paymentMethod.ExportPayments.Count>0 ||
                paymentMethod.ImportPayments.Count>0 ||
                paymentMethod.PurchaseBills.Count>0 ||
                paymentMethod.PurchaseReturnsBills.Count >0 ||
                paymentMethod.SaleBills.Count>0 ||
                paymentMethod.SalesReturnsBills.Count>0)
            {
                return BadRequest();
            }

            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentMethodExists(int id)
        {
            return _context.PaymentMethods.Any(e => e.Id == id);
        }

        public bool IsPaymentMethodDataNotValid(PaymentMethod paymentMethod)
        {
            //--Validation 
            /*
                  ID INT IDENTITY(1,1),
                  Balance FLOAT,
                  [Name] VARCHAR(50) NOT NULL UNIQUE,
                  [Type] VARCHAR(10) NOT NULL, --TODO Check in(Bank ,MoneySafe)
                  Constraint PaymentMethodPK PRIMARY KEY (ID),
                  Constraint TypeCheck Check ([Type] in ('Bank','MoneySafe') )
             */

            //foreign keys can not refer to Not Existed
            bool TypeCheckExisted = _context.PaymentMethods.Any(p => (p.Type == "Bank")||p.Type== "MoneySafe");

            // UNIQUE Prorerty must to be Not Existed before                                               
            bool IDExisted = _context.PaymentMethods.Any((p) => p.Id == paymentMethod.Id && p.Id != paymentMethod.Id);
            bool NameExisted = _context.PaymentMethods.Any((p) => p.Name == paymentMethod.Name && p.Name != paymentMethod.Name);

            //Not NUll properties + chech Foreign and Uniqe result
            if ( //if with OR:|| if any one true do If`s body  - if(condition){body} 
                paymentMethod.Id <= 0 ||
                paymentMethod.Name == null ||
                paymentMethod.Type == null || //again because it have both not null and unique 
                IDExisted ||//check null values but for numeric types
                NameExisted || // send bad request-NotValid- when foreign key Not Existed 
                !TypeCheckExisted 
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
