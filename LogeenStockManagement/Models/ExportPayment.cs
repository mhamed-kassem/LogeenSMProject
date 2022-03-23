using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ExportPayment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double PayedBalance { get; set; }
        public string CheckNumber { get; set; }
        public int PurchaseBillId { get; set; }
        public int SupplierId { get; set; }
        public int PayMethodId { get; set; }

        public virtual PaymentMethod PayMethod { get; set; }
        public virtual PurchaseBill PurchaseBill { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
