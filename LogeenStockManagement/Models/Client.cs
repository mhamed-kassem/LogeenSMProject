using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Client
    {
        public Client()
        {
            ImportPayments = new HashSet<ImportPayment>();
            SaleBills = new HashSet<SaleBill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? BalanceOutstand { get; set; }
        public decimal Phone { get; set; }
        public string Address { get; set; }
        public string TradeName { get; set; }
        public int? TypeId { get; set; }

        [JsonIgnore]
        public virtual TraderType Type { get; set; }
        [JsonIgnore]
        public virtual ICollection<ImportPayment> ImportPayments { get; set; }
        [JsonIgnore]
        public virtual ICollection<SaleBill> SaleBills { get; set; }
    }
}
