using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class ResidenceRequest
    {
        /// <summary>
        /// City of the person
        /// </summary>
        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;

        /// <summary>
        /// Street of the person
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Street { get; set; } = null!;

        /// <summary>
        /// House number of the person
        /// </summary>
        [Required]
        public int HouseNumber { get; set; }

        /// <summary>
        /// Room number of the person
        /// </summary>
        [Required]
        public int RoomNumber { get; set; }
    }
}
