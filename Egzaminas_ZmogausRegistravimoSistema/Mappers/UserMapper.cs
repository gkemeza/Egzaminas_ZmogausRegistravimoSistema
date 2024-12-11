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

        public User Map(SignUpRequest dto)
        {
            _authService.CreatePasswordHash(dto.Password!, out var passwordHash, out var passwordSalt);
            return new User
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PersonInfo = new PersonInfo
                {
                    FirstName = dto.PersonInfo.FirstName,
                    LastName = dto.PersonInfo.LastName,
                    PersonalId = dto.PersonInfo.PersonalId,
                    PhoneNumber = dto.PersonInfo.PhoneNumber,
                    Email = dto.PersonInfo.Email,
                    Residence = new Residence
                    {
                        City = dto.PersonInfo.Residence.City,
                        Street = dto.PersonInfo.Residence.Street,
                        HouseNumber = dto.PersonInfo.Residence.HouseNumber,
                        RoomNumber = dto.PersonInfo.Residence.RoomNumber
                    }
                }
            };
        }
    }
}
