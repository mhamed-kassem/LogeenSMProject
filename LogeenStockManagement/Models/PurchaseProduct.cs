﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class PurchaseProduct
    {
        public PurchaseProduct()
        {
            ReturnPurchaseProducts = new HashSet<ReturnPurchaseProduct>();
        }

        public int Id { get; set; }
        public int Amount { get; set; }
        public double? Discount { get; set; }
        public double? Price { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ProductId { get; set; }
        public int PurchaseBillId { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual PurchaseBill PurchaseBill { get; set; }
        [JsonIgnore]
        public virtual ICollection<ReturnPurchaseProduct> ReturnPurchaseProducts { get; set; }
    }
}
