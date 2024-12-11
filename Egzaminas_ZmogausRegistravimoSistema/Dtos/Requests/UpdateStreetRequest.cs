using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateStreetRequest
    {
        [Required]
        [StringLength(100)]
        public string NewStreet { get; set; } = null!;
    }
}
