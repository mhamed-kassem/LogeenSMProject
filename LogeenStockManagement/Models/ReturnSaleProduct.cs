using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ReturnSaleProduct
    {
        public int Id { get; set; }
        public int AmountReturned { get; set; }
        public int? SaleProductId { get; set; }
        public int ReturnSaleBillId { get; set; }

        [JsonIgnore]
        public virtual SalesReturnsBill ReturnSaleBill { get; set; }
        [JsonIgnore]
        public virtual SaleBillProduct SaleProduct { get; set; }
    }
}
