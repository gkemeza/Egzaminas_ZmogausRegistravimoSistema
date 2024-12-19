using Egzaminas_ZmogausRegistravimoSistema.Validators;
using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class SignUpRequest
    {
        /// <summary>
        /// Username of the user
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        /// <summary>
        /// Password of the user
        /// </summary>
        [PasswordValidator]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Person Info of the user
        /// </summary>
        [Required]
        public PersonInfoRequest PersonInfo { get; set; } = null!;
    }
}
