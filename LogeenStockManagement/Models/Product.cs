using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Product
    {
        public Product()
        {
            ExpiredProducts = new HashSet<ExpiredProduct>();
            ProductTransfereds = new HashSet<ProductTransfered>();
            PurchaseProducts = new HashSet<PurchaseProduct>();
            SaleBillProducts = new HashSet<SaleBillProduct>();
            StockProducts = new HashSet<StockProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MiniAmount { get; set; }
        public string Barcode { get; set; }
        public double SellingPrice { get; set; }
        public double PurchasingPrice { get; set; }
        public int ExpiryPeriod { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ExpiredProduct> ExpiredProducts { get; set; }
        public virtual ICollection<ProductTransfered> ProductTransfereds { get; set; }
        public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; }
        public virtual ICollection<SaleBillProduct> SaleBillProducts { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
    }
}
