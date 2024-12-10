using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateUsernameRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string NewUsername { get; set; } = null!;
    }
}
