using Egzaminas_ZmogausRegistravimoSistema.Database;
using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Egzaminas_ZmogausRegistravimoSistema.Repositories;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egzaminas_ZmogausRegistravimoSistemaTests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly PersonRegistrationContext _context;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PersonRegistrationContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
            .Options;
            _context = new PersonRegistrationContext(options);
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public void CreateUser_ValidUser_ReturnsNonNullId()
        {
            // Arrange
            var user = GetValidUser();

            // Act
            var result = _userRepository.CreateUser(user);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public void Create_DuplicateUsername_ThrowsException()
        {
            // Arrange
            var user1 = GetValidUser();
            var user2 = GetValidUser();

            // Act
            _userRepository.CreateUser(user1);

            // Assert
            Assert.Throws<InvalidOperationException>(() => _userRepository.CreateUser(user2));
        }

        [Fact]
        public void GetUserByUsername_ValidUsername_ReturnsCorrectUser()
        {
            // Arrange
            var user = GetValidUser();
            _userRepository.CreateUser(user);

            // Act
            var result = _userRepository.GetUserByUsername(user.Username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
        }

        [Fact]
        public void GetUserByUsername_InvalidUsername_ReturnsNull()
        {
            // Act
            var result = _userRepository.GetUserByUsername("invalidUserName");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Delete_ValidId_UserDoesNotExist()
        {
            // Arrange
            var user = GetValidUser();
            _userRepository.CreateUser(user);

            var id = user.Id;

            // Act
            _userRepository.DeleteUser(id);

            // Assert
            Assert.False(_userRepository.UserExists(id));
        }

        [Fact]
        public void Delete_InvalidId_NoExceptionThrown()
        {
            // Arrange
            var invalidId = Guid.NewGuid();

            // Act & Assert
            var exception = Record.Exception(() => _userRepository.DeleteUser(invalidId));
            Assert.Null(exception);
            Assert.False(_userRepository.UserExists(invalidId));
        }

        [Fact]
        public void UpdateUser_ValidUser_UpdatesUserDetails()
        {
            // Arrange
            var user = GetValidUser();
            _userRepository.CreateUser(user);

            user.Username = "UpdatedUser";

            // Act
            _userRepository.UpdateUser(user);

            // Assert
            var updatedUser = _context.Users.First();
            Assert.Equal("UpdatedUser", updatedUser.Username);
        }

        [Fact]
        public void UpdateUser_UserIsNull_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _userRepository.UpdateUser(null));
        }

        [Fact]
        public void UpdateUser_UpdatingNonExistingUser_DoesntCreateUser()
        {
            // Arrange
            var user = GetValidUser();
            _userRepository.CreateUser(user);

            var newUser = GetValidUser2();

            // Act
            try
            {
                _userRepository.UpdateUser(newUser);
            }
            catch (DbUpdateConcurrencyException) { }

            // Assert
            var existingUsers = _context.Users.ToList();
            Assert.Single(existingUsers);
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

        private User GetValidUser2()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = "Jonas",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                PersonInfo = new PersonInfo
                {
                    Id = 2,
                    FirstName = "Jonas",
                    LastName = "Jonaitis",
                    PersonalIdNumber = 41102126707,
                    PhoneNumber = "+37012548730",
                    Email = "jonas@gmail.com",
                    PhotoPath = "path/photo.png",
                    Residence = new Residence
                    {
                        Id = 2,
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
