using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExpiredProduct> ExpiredProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductTransfered> ProductTransfereds { get; set; }
        [JsonIgnore]
        public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<SaleBillProduct> SaleBillProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<StockProduct> StockProducts { get; set; }
    }
}
