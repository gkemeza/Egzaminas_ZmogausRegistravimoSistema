using Egzaminas_ZmogausRegistravimoSistema.Validators;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePersonalIdNumberRequest
    {
        /// <summary>
        /// New personal id number of the person
        /// </summary>
        [PersonalIdNumberValidator]
        public long NewPersonalIdNumber { get; set; }
    }
}
