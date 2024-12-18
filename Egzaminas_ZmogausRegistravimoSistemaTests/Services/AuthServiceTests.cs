using Egzaminas_ZmogausRegistravimoSistema.Services;

namespace Egzaminas_ZmogausRegistravimoSistemaTests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public void VerifyPasswordHash_ValidPassword_ReturnsTrue()
        {
            // Arrange
            var password = "validPassword";
            byte[] passwordHash;
            byte[] passwordSalt;
            var authService = new AuthService();

            authService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act
            var result = authService.VerifyPasswordHash(password, passwordHash, passwordSalt);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPasswordHash_IncorrectHashAndSalt_ReturnsFalse()
        {
            // Arrange
            var password = "validPassword";
            byte[] passwordHash;
            byte[] passwordSalt;
            var authService = new AuthService();

            authService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act
            var result = authService.VerifyPasswordHash("wrongPassword", passwordHash, passwordSalt);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPasswordHash_NullPassword_ThrowsException()
        {
            // Arrange
            var authService = new AuthService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => authService.VerifyPasswordHash(null, new byte[0], new byte[0]));
        }

        [Fact]
        public void VerifyPasswordHash_NullHashAndSalt_ThrowsException()
        {
            // Arrange
            var authService = new AuthService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => authService.VerifyPasswordHash("validPassword", null, null));
        }

        [Fact]
        public void CreatePasswordHash_ValidPassword_ReturnsNonNullHashAndSalt()
        {
            // Arrange
            var password = "validPassword";
            var authService = new AuthService();

            // Act
            authService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            // Assert
            Assert.NotNull(passwordHash);
            Assert.NotNull(passwordSalt);
            Assert.NotEmpty(passwordHash);
            Assert.NotEmpty(passwordSalt);
        }

        [Fact]
        public void CreatePasswordHash_NullPassword_ThrowsException()
        {
            // Arrange
            var authService = new AuthService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => authService.CreatePasswordHash(null, out _, out _));
        }

        [Fact]
        public void CreatePasswordHash_SamePasswords_ReturnsDifferentHashAndSalt()
        {
            // Arrange
            var password1 = "validPassword1";
            var password2 = "validPassword1";
            var authService = new AuthService();

            // Act
            authService.CreatePasswordHash(password1, out byte[] passwordHash1, out byte[] passwordSalt1);
            authService.CreatePasswordHash(password2, out byte[] passwordHash2, out byte[] passwordSalt2);

            // Assert
            Assert.NotEqual(passwordHash1, passwordHash2);
            Assert.NotEqual(passwordSalt1, passwordSalt2);
        }

        [Fact]
        public void CreatePasswordHash_DifferentPasswords_ReturnsDifferentHashAndSalt()
        {
            // Arrange
            var password1 = "validPassword1";
            var password2 = "validPassword2";
            var authService = new AuthService();

            // Act
            authService.CreatePasswordHash(password1, out byte[] passwordHash1, out byte[] passwordSalt1);
            authService.CreatePasswordHash(password2, out byte[] passwordHash2, out byte[] passwordSalt2);

            // Assert
            Assert.NotEqual(passwordHash1, passwordHash2);
            Assert.NotEqual(passwordSalt1, passwordSalt2);
        }

    }
}
