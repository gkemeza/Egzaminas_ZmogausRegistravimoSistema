using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePhotoRequest
    {
        [Required]
        public IFormFile NewPhoto { get; set; } = null!;
    }
}
