using Egzaminas_ZmogausRegistravimoSistema.Entities;

namespace Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Guid CreateUser(UserInfo user);
        void DeleteUser(Guid id);
        UserInfo? GetUserByUsername(string username);
    }
}
