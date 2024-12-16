using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Validators
{
    public class PersonalIdNumberValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Personal ID number is required.");
            }

            if (value is not long personalIdNumber)
            {
                return new ValidationResult("Personal ID number must be numeric.");
            }

            string personalIdStr = personalIdNumber.ToString();

            if (personalIdStr.Length != 11)
            {
                return new ValidationResult("Personal ID number must be exactly 11 digits.");
            }

            if (!long.TryParse(personalIdStr, out _))
            {
                return new ValidationResult("Personal ID number contains invalid characters.");
            }

            if (!IsValidLithuanianPersonalIdChecksum(personalIdStr))
            {
                return new ValidationResult("Invalid Personal ID number checksum.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidLithuanianPersonalIdChecksum(string personalId)
        {
            int[] weights1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4 };

            int sum = 0;

            // First iteration weights
            for (int i = 0; i < 10; i++)
            {
                sum += (personalId[i] - '0') * weights1[i];
            }

            int checksum = sum % 11;

            if (checksum == 10)
            {
                // Second iteration weights
                sum = 0;
                for (int i = 0; i < 10; i++)
                {
                    sum += (personalId[i] - '0') * weights2[i];
                }

                checksum = sum % 11;

                if (checksum == 10)
                {
                    checksum = 0;
                }
            }

            return checksum == (personalId[10] - '0');
        }
    }
}
