using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ReturnPurchaseProduct
    {
        public int Id { get; set; }
        public int AmountReturned { get; set; }
        public int? PurchaseProductId { get; set; }
        public int ReturnPurchaseBillId { get; set; }

        [JsonIgnore]
        public virtual PurchaseProduct PurchaseProduct { get; set; }
        [JsonIgnore]
        public virtual PurchaseReturnsBill ReturnPurchaseBill { get; set; }
    }
}
