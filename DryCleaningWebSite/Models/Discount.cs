using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public bool IsDeprecated { get; set; }

        public List<Client> Clients { get; set; }
    }
}
