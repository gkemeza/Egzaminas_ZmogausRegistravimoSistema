using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;

        // acknowledgment to developers and the compiler that the property is non-nullable but defers initialization to a later stage.
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Role { get; set; } = "User";

        public PersonInfo PersonInfo { get; set; } = null!;
    }
}
