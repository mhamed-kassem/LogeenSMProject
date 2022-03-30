using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ProductTransfered
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ProductId { get; set; }
        public int TransferOperationId { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual TransferOperation TransferOperation { get; set; }
    }
}
