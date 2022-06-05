using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryCleaningWebSite.Models;

namespace DryCleaningWebSite.ViewModils
{
    public class ClientViewModel
    {
        public SelectList DiscountList { get; set; }
        public Client ClientData { get; set; }
    }
}
