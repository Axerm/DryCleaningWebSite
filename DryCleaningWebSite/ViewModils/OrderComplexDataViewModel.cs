using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryCleaningWebSite.Models;

namespace DryCleaningWebSite.ViewModils
{
    public class OrderComplexDataViewModel
    {
        public SelectList ClientsData { get; set; }
        public SelectList ThingTypesData { get; set; }
        public SelectList ManufacturerMarkingsData { get; set; }
        public SelectList PollutionDegreeData { get; set; }
        public SelectList BurnoutDegreeData { get; set; }
        public SelectList GluePartssData { get; set; }
        public SelectList OperationalWearDegreesData { get; set; }
        public SelectList ServiceTypesData { get; set; }
        public SelectList StocksData { get; set; }
        public OrderDataViewModel OrderViewModelData { get; set; }
    }
}
