using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ImportPayment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double PayedBalance { get; set; }
        public string CheckNumber { get; set; }
        public string SaleBillCode { get; set; }

        public virtual SaleBill SaleBillCodeNavigation { get; set; }
    }
}
