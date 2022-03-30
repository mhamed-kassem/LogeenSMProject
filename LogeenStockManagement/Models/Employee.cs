using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Expenses = new HashSet<Expense>();
            TransferOperations = new HashSet<TransferOperation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal? NationalId { get; set; }
        public decimal? Phone { get; set; }
        public double Salary { get; set; }
        public string Photo { get; set; }
        public bool HaveAccess { get; set; }
        public int StockId { get; set; }
        public int JobId { get; set; }

        public virtual Job Job { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<TransferOperation> TransferOperations { get; set; }
    }
}
