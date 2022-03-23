using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            ExportPayments = new HashSet<ExportPayment>();
            PurchaseBills = new HashSet<PurchaseBill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Phone { get; set; }
        public string Address { get; set; }
        public double? BalanceDebit { get; set; }
        public int TypeId { get; set; }

        public virtual TraderType Type { get; set; }
        public virtual ICollection<ExportPayment> ExportPayments { get; set; }
        public virtual ICollection<PurchaseBill> PurchaseBills { get; set; }
    }
}
