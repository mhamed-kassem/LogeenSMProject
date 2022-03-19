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
        public string PurchaseBillCode { get; set; }

        public virtual PurchaseBill PurchaseBillCodeNavigation { get; set; }
    }
}
