using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Dtos.Results;
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

        public PersonInfoResult Map(User user)
        {
            return new PersonInfoResult
            {
                Id = user.PersonInfo.Id,
                FirstName = user.PersonInfo.FirstName,
                LastName = user.PersonInfo.LastName,
                PersonalId = user.PersonInfo.PersonalId,
                PhoneNumber = user.PersonInfo.PhoneNumber,
                Email = user.PersonInfo.Email,
                Residence = new ResidenceResult
                {
                    Id = user.PersonInfo.Residence.Id,
                    City = user.PersonInfo.Residence.City,
                    Street = user.PersonInfo.Residence.Street,
                    HouseNumber = user.PersonInfo.Residence.HouseNumber,
                    RoomNumber = user.PersonInfo.Residence.RoomNumber
                }
            };
        }
    }
}