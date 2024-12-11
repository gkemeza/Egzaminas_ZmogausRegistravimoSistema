using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateEmailRequest
    {
        [Required]
        public string NewEmail { get; set; } = null!;
    }
}
