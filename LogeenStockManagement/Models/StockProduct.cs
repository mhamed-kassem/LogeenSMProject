using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class StockProduct
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime ProductionDate { get; set; }
        public int? StockId { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
