using Egzaminas_ZmogausRegistravimoSistema.Database;
using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;

namespace Egzaminas_ZmogausRegistravimoSistema.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PersonRegistrationContext _context;

        public UserRepository(PersonRegistrationContext context)
        {
            _context = context;
        }

        public Guid CreateUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var exists = _context.UserInfos.Any(u => u.Username == user.Username);
            if (exists)
                throw new ArgumentException("Username already exists");

            _context.UserInfos.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public User? GetUserByUsername(string username)
        {
            ArgumentNullException.ThrowIfNull(username);

            return _context.UserInfos.FirstOrDefault(u => u.Username == username);
        }

        public void DeleteUser(Guid id)
        {
            var user = _context.UserInfos.Find(id);
            if (user != null)
            {
                _context.UserInfos.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
