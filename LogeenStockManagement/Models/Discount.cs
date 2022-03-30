using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Clients = new HashSet<Client>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public double DiscountValue { get; set; }
        public string Notes { get; set; }
        public int? UnitCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
