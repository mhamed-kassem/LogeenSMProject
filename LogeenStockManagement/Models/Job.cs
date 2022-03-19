using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Job
    {
        public Job()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
