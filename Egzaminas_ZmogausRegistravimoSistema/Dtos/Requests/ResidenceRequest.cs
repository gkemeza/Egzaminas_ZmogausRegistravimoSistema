using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class ResidenceRequest
    {
        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Street { get; set; } = null!;
        [Required]
        public int HouseNumber { get; set; }
        [Required]
        public int RoomNumber { get; set; }
    }
}
