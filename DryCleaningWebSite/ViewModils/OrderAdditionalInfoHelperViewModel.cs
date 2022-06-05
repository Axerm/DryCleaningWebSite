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
    public class OrderAdditionalInfoHelperViewModel
    {
        [Display(Name = "Номер наряд-заказа")]
        public int Id { get; set; }

        [Display(Name = "К оплате")]
        public decimal FinalPriceValue { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy}")]
        [DateAttributeCustomRatherNowCheck]
        [Display(Name = "Изменённая дата выдачи")]
        public DateTime? DateOfAffectedPlainIssue { get; set; }

        [Display(Name = "Фактическая дата выдачи")]
        public DateTime? DateOfIssue { get; set; }

        [Display(Name = "Статус оплаты")]
        public int StatusPayListId { get; set; }

        [Display(Name = "Статус наряд-заказа")]
        public int StatusOrderListId { get; set; }

        [Display(Name = "Дополнительная информация")]
        [StringLength(256, ErrorMessage = "Замтека должна содержать менее 256 символов")]
        public string OrderAdditionalInfo { get; set; }
    }
}
