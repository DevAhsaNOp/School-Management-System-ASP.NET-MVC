using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace SchoolManagementSystem.Helper
{
    public class RequireAtLeastOneItemAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as List<SelectListItem>;
            if (list != null && list.Any(i => i.Selected))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please select at least one subject.");
        }
    }

}