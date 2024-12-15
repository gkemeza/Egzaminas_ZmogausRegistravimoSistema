using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Validators
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // value is IFormFile file
            if (value is not string fileName)
            {
                return ValidationResult.Success;
            }

            foreach (var extension in _extensions)
            {
                if (fileName.EndsWith(extension))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult($"File extension not allowed. Allowed extensions: {string.Join(", ", _extensions)}");
        }
    }
}
