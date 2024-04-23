﻿using CheckYourEligibility_FrontEnd.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CheckYourEligibility_FrontEnd.Attributes
{
    public class NassAttribute : ValidationAttribute
    {
        private static readonly string NassPattern = @"^(?:[0-9]{2})(?:0[1-9]|1[0-2])";
        private static readonly Regex regex = new Regex(NassPattern);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (ParentDetailsViewModel)validationContext.ObjectInstance;

            if (model.IsNassSelected == true)
            {
                if (value == null || value == "")
                {
                    return new ValidationResult("Nass is required");
                }

                if (!regex.IsMatch(value.ToString()))
                {
                    return new ValidationResult("Nass field contains an invalid character");
                }
            }

            return ValidationResult.Success;
        }
    }
}
