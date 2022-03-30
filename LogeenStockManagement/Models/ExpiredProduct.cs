﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace LogeenStockManagement.Models
{
    public partial class ExpiredProduct
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Notes { get; set; }
        public int? ProductId { get; set; }
        public DateTime ProductionDate { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
