using Egzaminas_ZmogausRegistravimoSistema.Validators;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePhoneNumberRequest
    {
        [PhoneNumberValidator]
        public string NewPhoneNumber { get; set; } = null!;
    }
}
