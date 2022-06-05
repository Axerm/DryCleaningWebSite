using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DryCleaningWebSite.Attributes;
using DryCleaningWebSite.Models;

namespace DryCleaningWebSite.ViewModils
{
    public class OrderAdditionalInfoViewModel
    {
        [Display(Name = "Тип вещи с ценой")]
        public string ThingTypeValue { get; set; }

        [Display(Name = "Дополнительная стоимость")]
        public string AdditionalCostValue { get; set; }

        [Display(Name = "Скидка клиента")]
        public string DiscountValue { get; set; }

        [Display(Name = "Акция")]
        public string StockValue { get; set; }

        public SelectList StatusPayList { get; set; }

        public SelectList StatusOrderList { get; set; }

        public List<OrderAdditionalInfo> OrderAdditionalInfoList { get; set; }

        public OrderAdditionalInfoHelperViewModel OrderAdditionalInfoHelperViewModel { get; set; }
    }
}
