using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Models
{
    public class OrderAdditionalInfo
    {
        public int Id { get; set; }

        [StringLength(256)]
        public string Data { get; set; }
        public Employee Employee { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}
