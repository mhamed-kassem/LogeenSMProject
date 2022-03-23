using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class SaleBill
    {
        public SaleBill()
        {
            ImportPayments = new HashSet<ImportPayment>();
            SaleBillProducts = new HashSet<SaleBillProduct>();
            SalesReturnsBills = new HashSet<SalesReturnsBill>();
        }

        public int Id { get; set; }
        public string BillCode { get; set; }
        public DateTime Date { get; set; }
        public string BillType { get; set; }
        public double Discount { get; set; }
        public string CheckNumber { get; set; }
        public double? Paidup { get; set; }
        public double? Remaining { get; set; }
        public double? BillTotalPrice { get; set; }
        public int StockId { get; set; }
        public int PayMethodId { get; set; }
        public int? ClientId { get; set; }
        public int? TaxId { get; set; }

        public virtual Client Client { get; set; }
        public virtual PaymentMethod PayMethod { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Tax Tax { get; set; }
        public virtual ICollection<ImportPayment> ImportPayments { get; set; }
        public virtual ICollection<SaleBillProduct> SaleBillProducts { get; set; }
        public virtual ICollection<SalesReturnsBill> SalesReturnsBills { get; set; }
    }
}
