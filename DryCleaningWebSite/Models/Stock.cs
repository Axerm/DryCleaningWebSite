using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public bool IsDeprecated { get; set; }

        public List<Order> Orders { get; set; }
    }
}
