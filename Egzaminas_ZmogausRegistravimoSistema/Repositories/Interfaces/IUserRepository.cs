using Egzaminas_ZmogausRegistravimoSistema.Entities;

namespace Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Guid CreateUser(User user);
        void DeleteUser(Guid id);
        User? GetUserById(Guid id);
        User? GetUserByUsername(string username);
        bool UserExists(Guid id);
    }
}
