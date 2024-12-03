using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UserRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Username { get; set; }
        [PasswordValidator]
        public string? Password { get; set; }
    }
}
