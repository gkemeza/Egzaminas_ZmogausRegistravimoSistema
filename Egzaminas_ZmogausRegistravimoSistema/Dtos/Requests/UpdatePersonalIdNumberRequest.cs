using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePersonalIdNumberRequest
    {
        [Required]
        public long NewPersonalIdNumber { get; set; }
    }
}
