using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class TransferOperation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public int FromStockId { get; set; }
        public int ToStockId { get; set; }

        public virtual Stock FromStock { get; set; }
        public virtual Stock ToStock { get; set; }
    }
}
