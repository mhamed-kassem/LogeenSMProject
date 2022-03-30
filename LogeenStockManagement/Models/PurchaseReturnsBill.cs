using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class PurchaseReturnsBill
    {
        public PurchaseReturnsBill()
        {
            ReturnPurchaseProducts = new HashSet<ReturnPurchaseProduct>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public double? NetMoney { get; set; }
        public int PurchaseBillId { get; set; }
        public int? TaxId { get; set; }
        public int PayMethodId { get; set; }

        public virtual PaymentMethod PayMethod { get; set; }
        public virtual PurchaseBill PurchaseBill { get; set; }
        public virtual Tax Tax { get; set; }
        public virtual ICollection<ReturnPurchaseProduct> ReturnPurchaseProducts { get; set; }
    }
}
