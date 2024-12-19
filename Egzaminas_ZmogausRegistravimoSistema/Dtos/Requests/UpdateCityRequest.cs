using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateCityRequest
    {
        /// <summary>
        /// New city of the person
        /// </summary>
        [Required]
        [StringLength(100)]
        public string NewCity { get; set; } = null!;
    }
}
