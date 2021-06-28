using DryCleaningWebSite.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.ViewModils
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password, ErrorMessage = "Пароль указан некорректно")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

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

        [Required(ErrorMessage = "Не указан номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(ConstantValues.TelephoneNumberRegex, ErrorMessage = "Номер телефона указан не коррекнтно")]
        [Display(Name = "Номер телефона")]
        public string TelephoneNumber { get; set; }

        [Display(Name = "Филиал")]
        public int FilialId { get; set; }
        public SelectList FilialsList { get; set; }

        [Display(Name = "Роль пользователя")]
        public string RoleName { get; set; }
        public SelectList RoleNamesList { get; set; }
    }
}
