using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class PurchaseBill
    {
        public PurchaseBill()
        {
            ExportPayments = new HashSet<ExportPayment>();
            PurchaseReturnsBills = new HashSet<PurchaseReturnsBill>();
        }

        public string BillCode { get; set; }
        public DateTime Date { get; set; }
        public string BillType { get; set; }
        public double? Discount { get; set; }
        public double Paidup { get; set; }
        public string CheckNumber { get; set; }
        public double? BillTotal { get; set; }
        public double? Remaining { get; set; }
        public int StockId { get; set; }
        public int? TaxId { get; set; }
        public int PayMethodId { get; set; }
        public int? SupplierId { get; set; }

        public virtual PaymentMethod PayMethod { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Tax Tax { get; set; }
        public virtual ICollection<ExportPayment> ExportPayments { get; set; }
        public virtual ICollection<PurchaseReturnsBill> PurchaseReturnsBills { get; set; }
    }
}
