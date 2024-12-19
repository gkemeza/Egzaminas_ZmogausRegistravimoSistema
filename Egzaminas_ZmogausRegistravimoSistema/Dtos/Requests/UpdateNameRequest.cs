using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdateNameRequest
    {
        /// <summary>
        /// New first name of the person
        /// </summary>
        [StringLength(100)]
        public string? NewFirstName { get; set; }

        /// <summary>
        /// New last name of the person
        /// </summary>
        [StringLength(100)]
        public string? NewLastName { get; set; }
    }
}
