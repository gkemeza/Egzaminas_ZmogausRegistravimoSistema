using Egzaminas_ZmogausRegistravimoSistema.Entities;

namespace Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces
{
    public interface IJwtService
    {
        string GetJwtToken(User user);
    }
}
