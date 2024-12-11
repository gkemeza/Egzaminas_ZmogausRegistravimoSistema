using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egzaminas_ZmogausRegistravimoSistema.Entities
{
    public class PersonInfo
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public long PersonalId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Residence Residence { get; set; } = null!;
    }
}
