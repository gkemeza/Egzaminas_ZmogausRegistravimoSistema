using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;

namespace Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces
{
    public interface IUserService
    {
        Guid SignUp(SignUpRequest req);
    }
}
