using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Attributes
{
    public class DateAttributeCustomRatherNowCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            value = (DateTime)value;

            if (DateTime.Now.Date.CompareTo(value) <= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Не верно указана дата");
            }
        }
    }
}
