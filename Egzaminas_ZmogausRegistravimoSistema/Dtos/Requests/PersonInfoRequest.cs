using Egzaminas_ZmogausRegistravimoSistema.Validators;
using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class PersonInfoRequest
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [PersonalIdNumberValidator]
        public long PersonalIdNumber { get; set; }

        [PhoneNumberValidator]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress]
        [EmailValidator]
        public string Email { get; set; } = null!;

        [MaxPhotoSize(5 * 1024 * 1024)]
        [AllowedExtensions([".jpg", ".jpeg", ".png", ".bmp", ".gif"])]
        public IFormFile Photo { get; set; } = null!;

        [Required]
        public ResidenceRequest Residence { get; set; } = null!;

    }
}
