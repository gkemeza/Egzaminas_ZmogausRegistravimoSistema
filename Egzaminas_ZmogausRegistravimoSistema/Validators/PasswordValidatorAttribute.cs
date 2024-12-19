using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PasswordValidatorAttribute : ValidationAttribute
    {
        private readonly int _minimumLength = 8;
        private readonly int _maximumLength = 60;
        private readonly bool _requireUppercase = true;
        private readonly bool _requireLowercase = true;
        private readonly bool _requireDigit = true;
        private readonly bool _requireSpecialCharacter = true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult("Password is required.");

            var password = value.ToString();

            if (string.IsNullOrEmpty(password))
                return new ValidationResult("Password is required.");

            if (password.Length < _minimumLength)
            {
                return new ValidationResult($"Password must be at least {_minimumLength} characters long.");
            }

            if (password.Length > _maximumLength)
            {
                return new ValidationResult($"Password must be at most {_maximumLength} characters long.");
            }

            if (_requireUppercase && !password.Any(char.IsUpper))
            {
                return new ValidationResult("Password must contain at least one uppercase letter.");
            }

            if (_requireLowercase && !password.Any(char.IsLower))
            {
                return new ValidationResult("Password must contain at least one lowercase letter.");
            }

            if (_requireDigit && !password.Any(char.IsDigit))
            {
                return new ValidationResult("Password must contain at least one digit.");
            }

            if (_requireSpecialCharacter && password.All(char.IsLetterOrDigit))
            {
                return new ValidationResult("Password must contain at least one special character.");
            }

            return ValidationResult.Success;
        }
    }
}
