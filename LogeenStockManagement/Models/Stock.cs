using System;
using System.Collections.Generic;

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
            TransferOperationFromStocks = new HashSet<TransferOperation>();
            TransferOperationToStocks = new HashSet<TransferOperation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<PurchaseBill> PurchaseBills { get; set; }
        public virtual ICollection<SaleBill> SaleBills { get; set; }
        public virtual ICollection<TransferOperation> TransferOperationFromStocks { get; set; }
        public virtual ICollection<TransferOperation> TransferOperationToStocks { get; set; }
    }
}
