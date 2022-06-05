﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Models
{
    public class ThingType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeprecated { get; set; }
        public decimal Price { get; set; }

        public List<Order> Orders { get; set; }
    }
}