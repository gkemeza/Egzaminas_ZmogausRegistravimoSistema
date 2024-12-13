using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Dtos.Results;
using Egzaminas_ZmogausRegistravimoSistema.Entities;

namespace Egzaminas_ZmogausRegistravimoSistema.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User Map(SignUpRequest dto);
        PersonInfoResult Map(User user);
    }
}