using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual PaymentMethod PayMethod { get; set; }
        [JsonIgnore]
        public virtual PurchaseBill PurchaseBill { get; set; }
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
    }
}
