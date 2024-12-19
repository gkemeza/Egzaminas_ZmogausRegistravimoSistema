using Egzaminas_ZmogausRegistravimoSistema.Validators;
using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateEmailRequest
    {
        [EmailAddress]
        [EmailValidator]
        public string NewEmail { get; set; } = null!;
    }
}
