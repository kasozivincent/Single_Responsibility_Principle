using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Traderecord
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
    }
}
