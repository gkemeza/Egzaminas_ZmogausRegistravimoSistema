using AutoFixture;
using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Dtos.Results;
using Egzaminas_ZmogausRegistravimoSistema.Entities;
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
        public void GetUserPerson_ReturnsBadRequest_WhenIdIsEmpty()
        {
            // Arrange
            var emptyId = Guid.Empty;

            // Act
            var result = _controller.GetUserPerson(emptyId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid ID.", badRequestResult.Value);
        }

        [Fact]
        public void GetUserPerson_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns((User?)null);

            // Act
            var result = _controller.GetUserPerson(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void GetUserPerson_ReturnsOk_WhenUserExists()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var user = GetValidUser();
            var expectedPersonInfo = _fixture.Create<PersonInfoResult>();
            var photoBytes = _fixture.Create<byte[]>();

            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            _userMapperMock.Setup(mapper => mapper.Map(user)).Returns(expectedPersonInfo);
            _photoServiceMock.Setup(service => service.GetPhotoAsByteArray(user.PersonInfo.PhotoPath)).Returns(photoBytes);

            // Act
            var result = _controller.GetUserPerson(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPersonInfo = Assert.IsType<PersonInfoResult>(okResult.Value);

            Assert.Equal(expectedPersonInfo, returnedPersonInfo);
            Assert.Equal(photoBytes, returnedPersonInfo.PhotoBytes);
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

            // Act
            var result = _controller.SignUp(signUpRequest);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void Login_ReturnsOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginRequest = _fixture.Create<LoginRequest>();
            var user = GetValidUser();

            _userRepositoryMock.Setup(repo => repo.GetUserByUsername(loginRequest.Username))
            .Returns(user);

            _authServiceMock.Setup(auth => auth.VerifyPasswordHash(
                loginRequest.Password, user.PasswordHash, user.PasswordSalt))
                .Returns(true);

            var expectedToken = "test-jwt-token";
            _jwtServiceMock.Setup(jwt => jwt.GetJwtToken(user))
                .Returns(expectedToken);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnValue = okResult.Value;
            Assert.NotNull(returnValue);

            var tokenProperty = returnValue.GetType().GetProperty("Token");
            Assert.NotNull(tokenProperty);
            var actualToken = tokenProperty.GetValue(returnValue);

            Assert.Equal(expectedToken, actualToken);
        }

        [Fact]
        public void Login_UserNotFound_ReturnsBadRequest()
        {
            // Arrange
            var loginRequest = _fixture.Create<LoginRequest>();

            _userRepositoryMock.Setup(repo => repo.GetUserByUsername(loginRequest.Username))
                .Returns((User)null);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User not found", badRequestResult.Value);
        }

        [Fact]
        public void Login_InvalidPassword_ReturnsBadRequest()
        {
            // Arrange
            var loginRequest = _fixture.Create<LoginRequest>();
            var user = GetValidUser();

            _userRepositoryMock.Setup(repo => repo.GetUserByUsername(loginRequest.Username))
                .Returns(user);

            _authServiceMock.Setup(auth => auth.VerifyPasswordHash(
                loginRequest.Password,
                user.PasswordHash,
                user.PasswordSalt))
                .Returns(false);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid username or password", badRequestResult.Value);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            _userRepositoryMock.Setup(repo => repo.UserExists(userId)).Returns(false);

            // Act
            var result = _controller.Delete(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_WhenUserIsDeleted()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            _userRepositoryMock.Setup(repo => repo.UserExists(userId)).Returns(true);

            // Act
            var result = _controller.Delete(userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
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

        private User GetValidUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = "Jonas",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                PersonInfo = new PersonInfo
                {
                    Id = 1,
                    FirstName = "Jonas",
                    LastName = "Jonaitis",
                    PersonalIdNumber = 41102126707,
                    PhoneNumber = "+37012548730",
                    Email = "jonas@gmail.com",
                    PhotoPath = "path/photo.png",
                    Residence = new Residence
                    {
                        Id = 1,
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