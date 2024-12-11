using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePhoneNumberRequest
    {
        [Required]
        public string NewPhoneNumber { get; set; } = null!;
    }
}
