using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Expenses = new HashSet<Expense>();
            ExportPayments = new HashSet<ExportPayment>();
            ImportPayments = new HashSet<ImportPayment>();
            PurchaseBills = new HashSet<PurchaseBill>();
            PurchaseReturnsBills = new HashSet<PurchaseReturnsBill>();
            SaleBills = new HashSet<SaleBill>();
            SalesReturnsBills = new HashSet<SalesReturnsBill>();
        }

        public int Id { get; set; }
        public double? Balance { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<ExportPayment> ExportPayments { get; set; }
        public virtual ICollection<ImportPayment> ImportPayments { get; set; }
        public virtual ICollection<PurchaseBill> PurchaseBills { get; set; }
        public virtual ICollection<PurchaseReturnsBill> PurchaseReturnsBills { get; set; }
        public virtual ICollection<SaleBill> SaleBills { get; set; }
        public virtual ICollection<SalesReturnsBill> SalesReturnsBills { get; set; }
    }
}
