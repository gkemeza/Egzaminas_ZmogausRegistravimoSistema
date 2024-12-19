using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Mappers.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;

namespace Egzaminas_ZmogausRegistravimoSistema.Services
{
    public class UserService : IUserService
    {
        private readonly IUserMapper _userMapper;
        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;

        public UserService(IUserMapper userMapper,
            IPhotoService photoService, IUserRepository userRepository)
        {
            _userMapper = userMapper;
            _photoService = photoService;
            _userRepository = userRepository;
        }

        public Guid SignUp(SignUpRequest req)
        {
            var user = _userMapper.Map(req);

            var exists = _userRepository.UserExists(user.Id);
            if (exists)
            {
                throw new ArgumentException("Username already exists");
            }

            string photoPath = _photoService.GetPhotoPath(req.PersonInfo.Photo, "Uploads/Profile-pictures");
            user.PersonInfo.PhotoPath = photoPath;

            return _userRepository.CreateUser(user);
        }
    }
}
