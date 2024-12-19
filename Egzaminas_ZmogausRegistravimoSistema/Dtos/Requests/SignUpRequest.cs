using Egzaminas_ZmogausRegistravimoSistema.Validators;
using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class SignUpRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        [PasswordValidator]
        public string Password { get; set; } = null!;

        [Required]
        public PersonInfoRequest PersonInfo { get; set; } = null!;
    }
}
