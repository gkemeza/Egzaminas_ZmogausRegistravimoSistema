using Egzaminas_ZmogausRegistravimoSistema.Validators;
using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class PersonInfoRequest
    {
        /// <summary>
        /// First name of the person
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Last name of the person
        /// </summary>
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Personal id number of the person
        /// </summary>
        [PersonalIdNumberValidator]
        public long PersonalIdNumber { get; set; }

        /// <summary>
        /// Phone number of the person
        /// </summary>
        [PhoneNumberValidator]
        public string PhoneNumber { get; set; } = null!;

        /// <summary>
        /// Email of the person
        /// </summary>
        [EmailAddress]
        [EmailValidator]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Profile photo of the person
        /// </summary>
        [MaxPhotoSize(5 * 1024 * 1024)]
        [AllowedExtensions([".jpg", ".jpeg", ".png", ".bmp", ".gif"])]
        public IFormFile Photo { get; set; } = null!;

        /// <summary>
        /// Address of the person
        /// </summary>
        [Required]
        public ResidenceRequest Residence { get; set; } = null!;

    }
}
