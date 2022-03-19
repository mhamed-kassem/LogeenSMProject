using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ProductTransfered
    {
        public int Amount { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ProductId { get; set; }
        public int TransferOperationId { get; set; }

        public virtual Product Product { get; set; }
        public virtual TransferOperation TransferOperation { get; set; }
    }
}
