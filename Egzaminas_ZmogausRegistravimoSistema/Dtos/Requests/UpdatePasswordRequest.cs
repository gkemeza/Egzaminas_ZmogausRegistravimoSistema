using Egzaminas_ZmogausRegistravimoSistema.Validators;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePasswordRequest
    {
        /// <summary>
        /// Current password of the user
        /// </summary>
        public string CurrentPassword { get; set; } = null!;

        /// <summary>
        /// New password of the user
        /// </summary>
        [PasswordValidator]
        public string NewPassword { get; set; } = null!;
    }
}
