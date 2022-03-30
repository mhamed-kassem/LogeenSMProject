using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ReturnPurchaseProduct
    {
        public int Id { get; set; }
        public int AmountReturned { get; set; }
        public int? PurchaseProductId { get; set; }
        public int ReturnPurchaseBillId { get; set; }

        public virtual PurchaseProduct PurchaseProduct { get; set; }
        public virtual PurchaseReturnsBill ReturnPurchaseBill { get; set; }
    }
}
