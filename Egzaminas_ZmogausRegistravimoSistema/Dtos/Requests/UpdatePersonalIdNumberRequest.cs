using Egzaminas_ZmogausRegistravimoSistema.Validators;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePersonalIdNumberRequest
    {
        [PersonalIdNumberValidator]
        public long NewPersonalIdNumber { get; set; }
    }
}
