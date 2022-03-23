using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class SaleBillProduct
    {
        public int Id { get; set; }
        public int AmountToSell { get; set; }
        public double? Discount { get; set; }
        public double? TotalPrice { get; set; }
        public int SaleBillId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual SaleBill SaleBill { get; set; }
    }
}
