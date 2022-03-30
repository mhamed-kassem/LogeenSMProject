using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class PurchaseBill
    {
        public PurchaseBill()
        {
            ExportPayments = new HashSet<ExportPayment>();
            PurchaseProducts = new HashSet<PurchaseProduct>();
            PurchaseReturnsBills = new HashSet<PurchaseReturnsBill>();
        }

        public int Id { get; set; }
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

        [JsonIgnore]
        public virtual PaymentMethod PayMethod { get; set; }
        [JsonIgnore]
        public virtual Stock Stock { get; set; }
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
        [JsonIgnore]
        public virtual Tax Tax { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExportPayment> ExportPayments { get; set; }
        
        public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<PurchaseReturnsBill> PurchaseReturnsBills { get; set; }
    }
}
