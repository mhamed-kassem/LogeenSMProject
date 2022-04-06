using LogeenStockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogeenStockManagement
{
    public class RecurringFUNs
    {
        private readonly static LogeenStockManagementContext _context = new LogeenStockManagementContext();
       

        public static void ExpirationCheck()
        {
            List<StockProduct> stockProducts = _context.StockProducts.ToList();

            foreach(StockProduct item in stockProducts)
            {
                Product product = _context.Products.Find(item.ProductId);
                
                DateTime expireDate = item.ProductionDate;
                expireDate.AddMonths(product.ExpiryPeriod);


                if(expireDate < DateTime.Today)
                {
                    ExpiredProduct expiredProduct = new ExpiredProduct()
                    {
                        Amount = item.Amount,
                        DateAdded = DateTime.Today,
                        Notes = "Expired Auto check",
                        ProductId = item.ProductId,
                        ProductionDate = item.ProductionDate
                    };

                    _context.ExpiredProducts.Add(expiredProduct);
                    _context.StockProducts.Remove(item);
                }

            }

            _context.SaveChanges();

        }
    }
}
