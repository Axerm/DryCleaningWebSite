using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Models
{
    public class Employee : IdentityUser
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string FatherName { get; set; }
        public int? FilialId { get; set; }
        public Filial Filial { get; set; }
        public bool IsDeprecated { get; set; }

        public List<OrderAdditionalInfo> OrderAdditionalInfos { get; set; }
        public List<Order> Orders { get; set; }
    }
}
