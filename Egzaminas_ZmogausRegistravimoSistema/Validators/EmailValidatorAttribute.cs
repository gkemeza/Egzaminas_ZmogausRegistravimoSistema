using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var email = value as string;
            if (!string.IsNullOrEmpty(email))
            {
                var atIndex = email.IndexOf('@');
                if (atIndex == -1)
                    return new ValidationResult("Invalid email address.");

                var dotIndex = email.LastIndexOf('.');
                if (dotIndex == -1)
                    return new ValidationResult("Invalid top-level domain.");

                if (dotIndex <= atIndex + 1)
                    return new ValidationResult("Invalid top-level domain.");

                if (dotIndex == email.Length - 1)
                    return new ValidationResult("Missing top-level domain.");
            }

            return ValidationResult.Success!;
        }
    }
}
