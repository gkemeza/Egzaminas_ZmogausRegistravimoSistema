using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Egzaminas_ZmogausRegistravimoSistema.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Egzaminas_ZmogausRegistravimoSistemaTests.Services
{
    public class PhotoServiceTests
    {

        private readonly PhotoService _photoService;

        public PhotoServiceTests()
        {
            _photoService = new PhotoService();
        }

        [Fact]
        public void UpdateUserPhoto_UserIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            User user = null;
            var mockFile = new Mock<IFormFile>();
            string uploadDirectory = "uploads/";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _photoService.UpdateUserPhoto(user, mockFile.Object, uploadDirectory));
            Assert.Equal("user", exception.ParamName);
        }
    }
}
