using System;
using System.Collections.Generic;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class TraderType
    {
        public TraderType()
        {
            Clients = new HashSet<Client>();
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
