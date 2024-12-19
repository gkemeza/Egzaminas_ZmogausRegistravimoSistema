using Egzaminas_ZmogausRegistravimoSistema.Validators;
using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class LoginRequest
    {
        /// <summary>
        /// Username of the user
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Username { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [PasswordValidator]
        public string? Password { get; set; }
    }
}
