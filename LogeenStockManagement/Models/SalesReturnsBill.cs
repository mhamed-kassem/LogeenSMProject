using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class SalesReturnsBill
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public double? Discount { get; set; }
        public double? NetMoney { get; set; }
        public string SaleBillCode { get; set; }
        public int? TaxId { get; set; }
        public int PayMethodId { get; set; }

        public virtual PaymentMethod PayMethod { get; set; }
        public virtual SaleBill SaleBillCodeNavigation { get; set; }
        public virtual Tax Tax { get; set; }
    }
}
