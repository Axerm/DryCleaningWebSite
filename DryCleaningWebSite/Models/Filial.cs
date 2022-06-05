using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Models
{
    public class Filial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsDeprecated { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Order> Orders { get; set; }
    }
}