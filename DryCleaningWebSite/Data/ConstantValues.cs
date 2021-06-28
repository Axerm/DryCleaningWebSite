using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Data
{
    public static class ConstantValues
    {
        public const string AdminRoleName = "admin";
        public const string ReseptionistRoleName = "reseptionist";
        public const string TechnologistRoleName = "technologist";
        public const string AllRoleNames = "admin, reseptionist, technologist";
        public const string TelephoneNumberRegex
            = @"^([0-9]{1})[- ]?\(?([0-9]{3})\)?[- ]?([0-9]{3})[- ]?([0-9]{2})[- ]?([0-9]{2})$";
    }
}