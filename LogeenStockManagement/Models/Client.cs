using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Client
    {
        public Client()
        {
            SaleBills = new HashSet<SaleBill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? BalanceOutstand { get; set; }
        public decimal Phone { get; set; }
        public string Address { get; set; }
        public string TradeName { get; set; }
        public int? TypeId { get; set; }

        public virtual TraderType Type { get; set; }
        public virtual ICollection<SaleBill> SaleBills { get; set; }
    }
}
