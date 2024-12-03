using Egzaminas_ZmogausRegistravimoSistema.Database;
using Egzaminas_ZmogausRegistravimoSistema.Entities;

namespace Egzaminas_ZmogausRegistravimoSistema.Repositories
{

    public interface IUserRepository
    {
        Guid CreateUser(UserInfo user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly PersonRegistrationContext _context;

        public UserRepository(PersonRegistrationContext context)
        {
            _context = context;
        }

        public Guid CreateUser(UserInfo user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var exists = _context.UserInfos.Any(u => u.Username == user.Username);
            if (exists)
                throw new ArgumentException("Username already exists");

            _context.UserInfos.Add(user);
            _context.SaveChanges();
            return user.Id;
        }


    }
}
