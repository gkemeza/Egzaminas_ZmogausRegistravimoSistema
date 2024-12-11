using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateRoomNumberRequest
    {
        [Required]
        public int NewRoomNumber { get; set; }
    }
}
