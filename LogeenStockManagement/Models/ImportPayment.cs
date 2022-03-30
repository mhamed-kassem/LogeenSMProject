using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ImportPayment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double PayedBalance { get; set; }
        public string CheckNumber { get; set; }
        public int SaleBillId { get; set; }
        public int ClientId { get; set; }
        public int PayMethodId { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }
        [JsonIgnore]
        public virtual PaymentMethod PayMethod { get; set; }
        [JsonIgnore]
        public virtual SaleBill SaleBill { get; set; }
    }
}
