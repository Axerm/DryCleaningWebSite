using DryCleaningWebSite.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 20 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 20 символов")]
        [Display(Name = "Фамилия")]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 20 символов")]
        [Display(Name = "Отчество")]
        public string FatherName { get; set; }

        [Display(Name = "Полное имя")]
        public string FullName { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Длина строки должна быть до 50 символов")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(ConstantValues.TelephoneNumberRegex, ErrorMessage = "Номер телефона указан некорректно")]
        [Display(Name = "Номер телефона")]
        public string TelephoneNumber { get; set; }

        [Display(Name = "Согласие на уведомление об акциях")]
        public bool IsNotifyPromotions { get; set; }

        [Display(Name = "Дисконтная система")]
        public int? DiscountId { get; set; }

        [Display(Name = "Не активен")]
        public bool IsDeprecated { get; set; }

        public Discount Discount { get; set; }

        public List<Order> Orders { get; set; }
    }
}