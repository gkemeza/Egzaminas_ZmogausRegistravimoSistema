using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateNameRequest
    {
        [StringLength(100)]
        public string? NewFirstName { get; set; }

        [StringLength(100)]
        public string? NewLastName { get; set; }
    }
}
