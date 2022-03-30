using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ReturnSaleProduct
    {
        public int Id { get; set; }
        public int AmountReturned { get; set; }
        public int? SaleProductId { get; set; }
        public int ReturnSaleBillId { get; set; }

        public virtual SalesReturnsBill ReturnSaleBill { get; set; }
        public virtual SaleBillProduct SaleProduct { get; set; }
    }
}
