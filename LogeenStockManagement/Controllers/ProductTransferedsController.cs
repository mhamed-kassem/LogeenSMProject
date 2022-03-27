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
    public class ProductTransferedsController : ControllerBase
    {
        private readonly LogeenStockManagementContext _context;

        public ProductTransferedsController(LogeenStockManagementContext context)
        {
            _context = context;
        }

        // GET: api/ProductTransfereds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTransfered>>> GetProductTransfereds()
        {
            return await _context.ProductTransfereds.ToListAsync();
        }

        // GET: api/ProductTransfereds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTransfered>> GetProductTransfered(int id)
        {
            var productTransfered = await _context.ProductTransfereds.FindAsync(id);

            if (productTransfered == null)
            {
                return NotFound();
            }

            return productTransfered;
        }

        //// PUT: api/ProductTransfereds/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProductTransfered(int id, ProductTransfered productTransfered)
        //{
        //    //validation section1

        //    if (id != productTransfered.Id || IsProductTransferedDataNotValid(productTransfered))
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(productTransfered).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductTransferedExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/ProductTransfereds
        
        [HttpPost]
        public async Task<ActionResult<ProductTransfered>> PostProductTransfered(ProductTransfered productTransfered)
        {
            //validation section2
            if (IsProductTransferedDataNotValid (productTransfered))
            {
                return BadRequest();

            }


            //TODO throw StockProduct Controller Actions
            StockProduct UpadeStokeProduct = _context.StockProducts.
                 Where(sp => sp.StockId == productTransfered.TransferOperation.FromStockId
                 && sp.ProductId == productTransfered.ProductId
                 && sp.ProductionDate == productTransfered.ProductionDate).FirstOrDefault();
            
            if (UpadeStokeProduct.Amount > productTransfered.Amount)
            {
                UpadeStokeProduct.Amount -= productTransfered.Amount;

            }
            else if (UpadeStokeProduct.Amount == productTransfered.Amount)
            {
                _context.StockProducts.Remove(UpadeStokeProduct);
            }
            else
            {
                return BadRequest();
            }

            StockProduct newStockProduct;

            if (StockProductExists(productTransfered.ProductId, productTransfered.TransferOperation.ToStockId, productTransfered.ProductionDate, out newStockProduct))
            {
                newStockProduct.Amount += productTransfered.Amount;
            }
            else
            {
                newStockProduct = new StockProduct
                {
                    ProductId = productTransfered.ProductId,
                    StockId = productTransfered.TransferOperation.ToStockId,
                    ProductionDate = productTransfered.ProductionDate,
                    Amount = productTransfered.Amount
                };

                _context.StockProducts.Add(newStockProduct);
            }
            //TODO throw StockProduct Controller Actions
            //
            _context.ProductTransfereds.Add(productTransfered);
            
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetProductTransfered", new { id = productTransfered.Id }, productTransfered);
        }

        // DELETE: api/ProductTransfereds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductTransfered(int id)
        {
            var productTransfered = await _context.ProductTransfereds.FindAsync(id);
            
            if (productTransfered == null)
            {
                return NotFound();
            }

            _context.ProductTransfereds.Remove(productTransfered);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool ProductTransferedExists(int id)
        //{
        //    return _context.ProductTransfereds.Any(e => e.Id == id);
        //}

        private bool StockProductExists(int ProductId, int StockId, DateTime ProductionDate,out StockProduct stockProduct)
        {
            stockProduct = _context.StockProducts.
                 Where(sp => sp.StockId == StockId
                 && sp.ProductId == ProductId
                 && sp.ProductionDate == ProductionDate).FirstOrDefault();

            if (stockProduct == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        

        public bool IsProductTransferedDataNotValid(ProductTransfered productTransfered)
        {
            /*
             * ID INT IDENTITY(1,1),
              Amount INT NOT NULL,
              ProductionDate DATE NOT NULL, --logic bring it from store products table or manully
              ProductId INT NOT NULL,
              TransferOperationId INT NOT NULL,
              CONSTRAINT ProductTransferedPK PRIMARY KEY (ID),
              CONSTRAINT ProductTransferedTypeFK FOREIGN KEY (ProductId) REFERENCES Product(ID),
              CONSTRAINT ProductTransferOPerationFK FOREIGN KEY (TransferOperationId) REFERENCES TransferOperation(ID)
             */

            //foreign keys can not refer to Not Existed
            bool ProductExisted = _context.Products.Any(p => p.Id == productTransfered.ProductId);
            bool TransferOperationExisted = _context.TransferOperations.Any(op => op.Id == productTransfered.TransferOperationId);

            //Logic 10-valid date and exixted in the Form-store by the same production date
            //5 valid date but not Existed in the From-store products 


            if (
                productTransfered.Amount <= 0 ||
                !ProductExisted || !TransferOperationExisted ||
                DateTime.TryParse(productTransfered.ProductionDate.ToString(), out _)
                )
            {
                return true;
            }
            else if (!_context.StockProducts.Any(p =>
                p.StockId == productTransfered.TransferOperation.FromStockId
                && p.ProductId == productTransfered.ProductId
                && p.ProductionDate == productTransfered.ProductionDate)
                )
            {
                return true;
            }

            else if (productTransfered.Amount > GetStockProductAmount(productTransfered.ProductId, productTransfered.TransferOperation.FromStockId))
            {
                return true;
            }else
            {
                return false;
            }

        }




        /// <summary>
        /// Move next Actions To StockProduct Controller
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="StockId"></param>
        /// <returns></returns>
        //[HttpGet("{ProductId,StockId}")]
        public IEnumerable<DateTime> GetProductionDates(int ProductId,int StockId)
        {
            return _context.StockProducts.Where(sp=>sp.StockId==StockId&&sp.ProductId==ProductId).Select(sp=>sp.ProductionDate);
        }

        //[HttpGet("{StockId}")]
        public IEnumerable<Product> GetStockProducts(int StockId)
        {
            return _context.Products.Where(p => _context.StockProducts.Any(sp => sp.StockId == StockId && sp.ProductId==p.Id)).ToList(); 
        }

        public int GetStockProductAmount(int ProductId, int StockId)
        {
            return _context.StockProducts.Where(sp => sp.StockId == StockId && sp.ProductId == ProductId).Select(sp=>sp.Amount).Sum();
        }


    }//Controller
}
