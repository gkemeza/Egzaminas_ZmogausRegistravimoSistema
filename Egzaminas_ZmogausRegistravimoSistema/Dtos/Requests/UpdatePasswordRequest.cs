using Egzaminas_ZmogausRegistravimoSistema.Validators;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePasswordRequest
    {
        public string CurrentPassword { get; set; } = null!;

        [PasswordValidator]
        public string NewPassword { get; set; } = null!;
    }
}
