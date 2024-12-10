using Egzaminas_ZmogausRegistravimoSistema.Database;
using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            var exists = _context.Users.Any(u => u.Username == user.Username);
            if (exists)
                throw new ArgumentException("Username already exists");

            _context.Users.Add(user);
            _context.PersonInfos.Add(user.PersonInfo);
            _context.Residences.Add(user.PersonInfo.Residence);
            _context.SaveChanges();
            return user.Id;
        }

        public User? GetUserByUsername(string username)
        {
            ArgumentNullException.ThrowIfNull(username);

            return _context.Users
                .Include(u => u.PersonInfo)
                .ThenInclude(pf => pf.Residence)
                .FirstOrDefault(u => u.Username == username);
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool UserExists(Guid id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
