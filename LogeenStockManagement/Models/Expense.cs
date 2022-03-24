using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public double Value { get; set; }
        public string CheckNumber { get; set; }
        public int StockId { get; set; }
        public int? EmployeeId { get; set; }
        public int TypeId { get; set; }
        public int PayMethodId { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual PaymentMethod PayMethod { get; set; }
        [JsonIgnore]
        public virtual Stock Stock { get; set; }
        [JsonIgnore]
        public virtual ExpenseType Type { get; set; }
    }
}
