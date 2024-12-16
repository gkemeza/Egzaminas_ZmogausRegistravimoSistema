using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Egzaminas_ZmogausRegistravimoSistema.Validators
{
    public class PhoneNumberValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string phoneNumber)
            {
                string pattern = @"^\+?[1-9]\d{1,14}$"; // E.164 format
                if (!Regex.IsMatch(phoneNumber, pattern))
                {
                    return new ValidationResult("Invalid phone number format.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
