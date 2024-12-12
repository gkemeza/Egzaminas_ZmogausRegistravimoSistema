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
        public long PersonalId { get; set; }
        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        // TODO: email validator
        public string Email { get; set; } = null!;
        //[MaxFileSize(5 * 1024 * 1024)]
        //[AllowedExtensions([".png"])]
        [Required]
        public IFormFile Photo { get; set; } = null!;

        [Required]
        public ResidenceRequest Residence { get; set; } = null!;
    }
}
