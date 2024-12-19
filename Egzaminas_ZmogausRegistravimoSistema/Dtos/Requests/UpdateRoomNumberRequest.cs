using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateRoomNumberRequest
    {
        /// <summary>
        /// New room number of the person
        /// </summary>
        [Required]
        public int NewRoomNumber { get; set; }
    }
}
