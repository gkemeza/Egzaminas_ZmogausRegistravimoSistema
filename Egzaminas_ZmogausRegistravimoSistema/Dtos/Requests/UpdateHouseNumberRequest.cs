using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateHouseNumberRequest
    {
        /// <summary>
        /// New house number of the person
        /// </summary>
        [Required]
        public int NewHouseNumber { get; set; }
    }
}
