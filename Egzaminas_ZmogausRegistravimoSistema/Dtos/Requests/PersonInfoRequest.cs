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
        [Required]
        public long PersonalIdNumber { get; set; }
        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; } = null!;
        [EmailAddress]
        [EmailValidator]
        public string Email { get; set; } = null!;
        //[Required]
        [MaxPhotoSize(5 * 1024 * 1024)]
        [AllowedExtensions([".jpg", ".jpeg", ".png", ".svg", ".tiff"])]
        public IFormFile Photo { get; set; } = null!;

        [Required]
        public ResidenceRequest Residence { get; set; } = null!;
    }
}
