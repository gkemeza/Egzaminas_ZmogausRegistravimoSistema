using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Validators
{
    public class MaxPhotoSizeAttribute : ValidationAttribute
    {
        private readonly int _maxPhotoSize;

        public MaxPhotoSizeAttribute(int maxFileSize)
        {
            _maxPhotoSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }
            if (file.Length > _maxPhotoSize)
            {
                return new ValidationResult($"File size should not be larger than {_maxPhotoSize} bytes");
            }
            return ValidationResult.Success;
        }
    }
}