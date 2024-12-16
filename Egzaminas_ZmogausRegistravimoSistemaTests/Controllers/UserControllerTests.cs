using AutoFixture;
using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Mappers.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Egzaminas_ZmogausRegistravimoSistema.Controllers.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserMapper> _userMapperMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IPhotoService> _photoServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;
        private readonly IFixture _fixture;

        public UserControllerTests()
        {
            _loggerMock = new Mock<ILogger<UserController>>();
            _authServiceMock = new Mock<IAuthService>();
            _userMapperMock = new Mock<IUserMapper>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _photoServiceMock = new Mock<IPhotoService>();
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_loggerMock.Object,
                _authServiceMock.Object, _userMapperMock.Object,
                _userRepositoryMock.Object, _jwtServiceMock.Object,
                _photoServiceMock.Object, _userServiceMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void SignUp_ReturnsBadRequest_WhenNoFileUploaded()
        {
            // Arrange
            var signUpRequest = _fixture.Build<SignUpRequest>()
                                         .With(x => x.PersonInfo, new PersonInfoRequest { Photo = null })
                                         .Create();

            // Act
            var result = _controller.SignUp(signUpRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file uploaded.", badRequestResult.Value);
        }

        [Fact]
        public void SignUp_ReturnsCreated_WhenUserIsSuccessfullyCreated()
        {
            // Arrange
            var signUpRequest = GetValidSignUpRequest();
            var expectedUserId = Guid.NewGuid();

            // Act
            var result = _controller.SignUp(signUpRequest);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
        }

        private SignUpRequest GetValidSignUpRequest()
        {
            var photoContent = new MemoryStream(new byte[] { 1, 2, 3, });

            return new SignUpRequest
            {
                Username = "Jonas",
                Password = "Jonas123",
                PersonInfo = new PersonInfoRequest
                {
                    FirstName = "Jonas",
                    LastName = "Jonaitis",
                    PersonalIdNumber = 41102126707,
                    PhoneNumber = "+37012548730",
                    Email = "jonas@gmail.com",
                    Photo = new FormFile(photoContent, 0, photoContent.Length, "file", "photo.jpg"),
                    Residence = new ResidenceRequest
                    {
                        City = "Vilnius",
                        Street = "Gedimino pr.",
                        HouseNumber = 10,
                        RoomNumber = 20
                    }
                }
            };
        }
    }
}