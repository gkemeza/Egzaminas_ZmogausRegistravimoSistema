using AutoFixture;
using Egzaminas_ZmogausRegistravimoSistema.Controllers;
using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace Egzaminas_ZmogausRegistravimoSistemaTests.Controllers
{
    public class ProfileControllerTests
    {
        private readonly Mock<ILogger<ProfileController>> _loggerMock;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IPhotoService> _photoServiceMock;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly ProfileController _controller;
        private readonly IFixture _fixture;
        private readonly Guid _testUserId;

        public ProfileControllerTests()
        {
            _loggerMock = new Mock<ILogger<ProfileController>>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockAuthService = new Mock<IAuthService>();
            _photoServiceMock = new Mock<IPhotoService>();

            _testUserId = Guid.NewGuid();

            var claims = new List<Claim>
         {
            new Claim(ClaimTypes.NameIdentifier, _testUserId.ToString())
        };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _mockHttpContext = new Mock<HttpContext>();
            _mockHttpContext.Setup(x => x.User).Returns(claimsPrincipal);

            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(_mockHttpContext.Object);

            _controller = new ProfileController(_loggerMock.Object, _mockUserRepository.Object,
                _mockAuthService.Object, _photoServiceMock.Object, _mockHttpContextAccessor.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void UpdateUsername_ValidRequest_ReturnsNoContent()
        {
            // Arrange
            var updateUsernameRequest = _fixture.Create<UpdateUsernameRequest>();
            var existingUser = GetValidUser();
            existingUser.Id = _testUserId;

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId))
                .Returns(existingUser);

            // Act
            var result = _controller.UpdateUsername(updateUsernameRequest);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(updateUsernameRequest.NewUsername, existingUser.Username);
        }

        [Fact]
        public void UpdateUsername_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var updateUsernameRequest = _fixture.Create<UpdateUsernameRequest>();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId))
                .Returns((User?)null);

            // Act
            var result = _controller.UpdateUsername(updateUsernameRequest);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdatePassword_ValidRequest_ReturnsNoContent()
        {
            // Arrange
            var updatePasswordRequest = _fixture.Create<UpdatePasswordRequest>();
            var existingUser = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId))
                .Returns(existingUser);

            _mockAuthService.Setup(auth => auth.VerifyPasswordHash(
                updatePasswordRequest.CurrentPassword,
                existingUser.PasswordHash,
                existingUser.PasswordSalt))
                .Returns(true);

            byte[] newPasswordHash = new byte[] { 7, 8, 9 };
            byte[] newPasswordSalt = new byte[] { 10, 11, 12 };
            _mockAuthService.Setup(auth => auth.CreatePasswordHash(
                updatePasswordRequest.NewPassword,
                out newPasswordHash,
                out newPasswordSalt))
                .Verifiable();

            // Act
            var result = _controller.UpdatePassword(updatePasswordRequest);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(newPasswordHash, existingUser.PasswordHash);
            Assert.Equal(newPasswordSalt, existingUser.PasswordSalt);
        }

        [Fact]
        public void UpdatePassword_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var updatePasswordRequest = _fixture.Create<UpdatePasswordRequest>();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId))
                .Returns((User?)null);

            // Act
            var result = _controller.UpdatePassword(updatePasswordRequest);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdatePassword_IncorrectCurrentPassword_ReturnsUnauthorized()
        {
            // Arrange
            var updatePasswordRequest = _fixture.Create<UpdatePasswordRequest>();
            var existingUser = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId))
                .Returns(existingUser);

            _mockAuthService.Setup(auth => auth.VerifyPasswordHash(
                updatePasswordRequest.CurrentPassword,
                existingUser.PasswordHash,
                existingUser.PasswordSalt))
                .Returns(false);

            // Act
            var result = _controller.UpdatePassword(updatePasswordRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Current password is incorrect", unauthorizedResult.Value);
        }

        [Fact]
        public void UpdatePersonalIdNumber_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var request = _fixture.Create<UpdatePersonalIdNumberRequest>();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns((User?)null);

            // Act
            var result = _controller.UpdatePersonalIdNumber(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdatePersonalIdNumber_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = _fixture.Create<UpdatePersonalIdNumberRequest>();
            var user = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns(user);

            // Act
            var result = _controller.UpdatePersonalIdNumber(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(request.NewPersonalIdNumber, user.PersonInfo.PersonalIdNumber);
        }

        [Fact]
        public void UpdatePhoneNumber_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var request = _fixture.Create<UpdatePhoneNumberRequest>();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns((User?)null);

            // Act
            var result = _controller.UpdatePhoneNumber(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdatePhoneNumber_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = _fixture.Create<UpdatePhoneNumberRequest>();
            var user = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns(user);

            // Act
            var result = _controller.UpdatePhoneNumber(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(request.NewPhoneNumber, user.PersonInfo.PhoneNumber);
        }

        [Fact]
        public void UpdateEmail_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var request = _fixture.Create<UpdateEmailRequest>();
            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns((User?)null);

            // Act
            var result = _controller.UpdateEmail(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdateEmail_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = _fixture.Create<UpdateEmailRequest>();
            var user = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns(user);

            // Act
            var result = _controller.UpdateEmail(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(request.NewEmail, user.PersonInfo.Email);
        }

        [Fact]
        public void UpdatePhoto_ReturnsBadRequest_WhenPhotoIsNull()
        {
            // Arrange
            var request = new UpdatePhotoRequest { NewPhoto = null };

            // Act
            var result = _controller.UpdatePhoto(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No photo uploaded.", badRequestResult.Value);
        }

        [Fact]
        public void UpdatePhoto_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var photoContent = new MemoryStream(new byte[] { 1, 2, 3, });
            var photoFile = new FormFile(photoContent, 0, photoContent.Length, "file", "test.jpg");
            var request = new UpdatePhotoRequest { NewPhoto = photoFile };

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns((User?)null);

            // Act
            var result = _controller.UpdatePhoto(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdatePhoto_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var photoContent = new MemoryStream(new byte[] { 1, 2, 3, });
            var photoFile = new FormFile(photoContent, 0, photoContent.Length, "file", "test.jpg");
            var request = new UpdatePhotoRequest { NewPhoto = photoFile };

            var user = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns(user);
            _photoServiceMock.Setup(service => service.GetPhotoPath(photoFile, "Uploads/Profile-pictures"))
                             .Returns("Uploads/Profile-pictures/test.jpg");

            // Act
            var result = _controller.UpdatePhoto(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCity_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var request = _fixture.Create<UpdateCityRequest>();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns((User?)null);

            // Act
            var result = _controller.UpdateCity(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdateCity_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = _fixture.Create<UpdateCityRequest>();
            var user = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns(user);

            // Act
            var result = _controller.UpdateCity(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(request.NewCity, user.PersonInfo.Residence.City);
        }

        [Fact]
        public void UpdateStreet_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var request = _fixture.Create<UpdateStreetRequest>();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns((User?)null);

            // Act
            var result = _controller.UpdateStreet(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdateStreet_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = _fixture.Create<UpdateStreetRequest>();
            var user = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns(user);

            // Act
            var result = _controller.UpdateStreet(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(request.NewStreet, user.PersonInfo.Residence.Street);
        }

        [Fact]
        public void UpdateHouseNumber_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var request = _fixture.Create<UpdateHouseNumberRequest>();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns((User?)null);

            // Act
            var result = _controller.UpdateHouseNumber(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdateHouseNumber_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = _fixture.Create<UpdateHouseNumberRequest>();
            var user = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns(user);

            // Act
            var result = _controller.UpdateHouseNumber(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(request.NewHouseNumber, user.PersonInfo.Residence.HouseNumber);
        }

        [Fact]
        public void UpdateRoomNumber_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var request = _fixture.Create<UpdateRoomNumberRequest>();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns((User?)null);

            // Act
            var result = _controller.UpdateRoomNumber(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void UpdateRoomNumber_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var request = _fixture.Create<UpdateRoomNumberRequest>();
            var user = GetValidUser();

            _mockUserRepository.Setup(repo => repo.GetUserById(_testUserId)).Returns(user);

            // Act
            var result = _controller.UpdateRoomNumber(request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(request.NewRoomNumber, user.PersonInfo.Residence.RoomNumber);
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
