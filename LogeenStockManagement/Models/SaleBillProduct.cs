using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class SaleBillProduct
    {
        public SaleBillProduct()
        {
            ReturnSaleProducts = new HashSet<ReturnSaleProduct>();
        }

        public int Id { get; set; }
        public int AmountToSell { get; set; }
        public double? Discount { get; set; }
        public DateTime ProductionDate { get; set; }
        public double? TotalPrice { get; set; }
        public int SaleBillId { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual SaleBill SaleBill { get; set; }
        [JsonIgnore]
        public virtual ICollection<ReturnSaleProduct> ReturnSaleProducts { get; set; }
    }
}
