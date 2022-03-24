using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Tax
    {
        public Tax()
        {
            PurchaseBills = new HashSet<PurchaseBill>();
            PurchaseReturnsBills = new HashSet<PurchaseReturnsBill>();
            SaleBills = new HashSet<SaleBill>();
            SalesReturnsBills = new HashSet<SalesReturnsBill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<PurchaseBill> PurchaseBills { get; set; }
        [JsonIgnore]
        public virtual ICollection<PurchaseReturnsBill> PurchaseReturnsBills { get; set; }
        [JsonIgnore]
        public virtual ICollection<SaleBill> SaleBills { get; set; }
        [JsonIgnore]
        public virtual ICollection<SalesReturnsBill> SalesReturnsBills { get; set; }
    }
}
