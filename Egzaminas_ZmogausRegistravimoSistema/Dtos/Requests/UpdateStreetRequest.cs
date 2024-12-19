using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateStreetRequest
    {
        /// <summary>
        /// New street number of the person
        /// </summary>
        [Required]
        [StringLength(100)]
        public string NewStreet { get; set; } = null!;
    }
}
