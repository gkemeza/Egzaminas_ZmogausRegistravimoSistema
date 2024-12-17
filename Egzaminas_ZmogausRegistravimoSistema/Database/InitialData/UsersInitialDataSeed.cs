using Egzaminas_ZmogausRegistravimoSistema.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Egzaminas_ZmogausRegistravimoSistema.Database.InitialData
{
    public static class UsersInitialDataSeed
    {
        public static List<User> Users => new()
        {
            GetUser1()
        };

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static User GetUser1()
        {
            CreatePasswordHash("User123@", out var passwordHash, out var passwordSalt);
            return new User
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Username = "User1",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "Admin"
            };
        }
    }
}
