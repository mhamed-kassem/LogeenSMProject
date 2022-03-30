using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
