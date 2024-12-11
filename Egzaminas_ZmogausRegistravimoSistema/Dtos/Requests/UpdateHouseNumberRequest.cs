using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateHouseNumberRequest
    {
        [Required]
        public int NewHouseNumber { get; set; }
    }
}
