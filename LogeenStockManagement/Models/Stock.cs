using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Stock
    {
        public Stock()
        {
            Employees = new HashSet<Employee>();
            Expenses = new HashSet<Expense>();
            PurchaseBills = new HashSet<PurchaseBill>();
            SaleBills = new HashSet<SaleBill>();
            StockProducts = new HashSet<StockProduct>();
            TransferOperationFromStocks = new HashSet<TransferOperation>();
            TransferOperationToStocks = new HashSet<TransferOperation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; }
        [JsonIgnore]
        public virtual ICollection<PurchaseBill> PurchaseBills { get; set; }
        [JsonIgnore]
        public virtual ICollection<SaleBill> SaleBills { get; set; }
        [JsonIgnore]
        public virtual ICollection<StockProduct> StockProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<TransferOperation> TransferOperationFromStocks { get; set; }
        [JsonIgnore]
        public virtual ICollection<TransferOperation> TransferOperationToStocks { get; set; }
    }
}
