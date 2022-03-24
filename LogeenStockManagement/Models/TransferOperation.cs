using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class TransferOperation
    {
        public TransferOperation()
        {
            ProductTransfereds = new HashSet<ProductTransfered>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public int EmployeeId { get; set; }
        public int FromStockId { get; set; }
        public int ToStockId { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual Stock FromStock { get; set; }
        [JsonIgnore]
        public virtual Stock ToStock { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductTransfered> ProductTransfereds { get; set; }
    }
}
