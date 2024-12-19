using Egzaminas_ZmogausRegistravimoSistema.Validators;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePhoneNumberRequest
    {
        /// <summary>
        /// New phone number of the person
        /// </summary>
        [PhoneNumberValidator]
        public string NewPhoneNumber { get; set; } = null!;
    }
}
