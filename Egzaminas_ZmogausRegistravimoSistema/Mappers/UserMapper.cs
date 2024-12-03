using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Egzaminas_ZmogausRegistravimoSistema.Mappers.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;

namespace Egzaminas_ZmogausRegistravimoSistema.Mappers
{
    public class UserMapper : IUserMapper
    {
        private readonly IAuthService _authService;

        public UserMapper(IAuthService authService)
        {
            _authService = authService;
        }

        public User Map(UserRequest dto)
        {
            _authService.CreatePasswordHash(dto.Password!, out var passwordHash, out var passwordSalt);
            return new User
            {
                Username = dto.Username!,
                Password = dto.Password!,
                PasswordSalt = passwordSalt,
                Role = dto.Role!
            };
        }
    }
}
