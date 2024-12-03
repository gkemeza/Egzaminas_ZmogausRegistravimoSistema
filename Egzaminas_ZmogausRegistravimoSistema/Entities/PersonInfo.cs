using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Entities
{
    public class PersonInfo
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int PersonalId { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        //public IFormFile ProfilePhoto { get; set; }
        public Residence Residence { get; set; } = null!;
    }
}
