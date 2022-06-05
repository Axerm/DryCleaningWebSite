using DryCleaningWebSite.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.ViewModils
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Новый пароль указан некорректно")]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия")]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        [Display(Name = "Отчество")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(ConstantValues.TelephoneNumberRegex, ErrorMessage = "Номер телефона указан некорректно")]
        [Display(Name = "Номер телефона")]
        public string TelephoneNumber { get; set; }

        [Display(Name = "Филиал")]
        public int FilialId { get; set; }
        public SelectList FilialsList { get; set; }
        
        [Display(Name = "Роль пользователя")]
        public string RoleName { get; set; }
        public SelectList RoleNamesList { get; set; }

        [Display(Name = "Отмечен на удаление")]
        public bool IsDeprecated { get; set; }
    }
}
