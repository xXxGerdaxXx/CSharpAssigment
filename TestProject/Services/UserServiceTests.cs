using Xunit;
using Business_Library.Services;
using Business_Library.Models;

namespace TestProject.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_ShouldAddUserSuccessfully()
        {
            // Arrange
            var userService = new UserService();
            var user = new UserBase
            {
                Name = "Jane",
                Surname = "Doe",
                Email = "jane.doe@example.com",
                Mobile = "987654321",
                Address = "456 Elm St",
                PostNumber = "54321",
                City = "Metropolis"
            };

            // Act
            userService.AddUser(user);
            var users = userService.GetAllUsers();

            // Assert
            Assert.Single(users);
            Assert.Contains(user, users);
        }

        [Fact]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            // Arrange
            var userService = new UserService();
            var user = new UserBase
            {
                Name = "Alice",
                Surname = "Smith",
                Email = "alice.smith@example.com"
            };

            // Add the user to the service, which generates the ID
            userService.AddUser(user);

            // Retrieve the generated ID after adding the user
            var generatedId = user.Id;

            // Act
            var retrievedUser = userService.GetUserById(generatedId);

            // Assert
            Assert.Equal(user, retrievedUser);
        }

        [Fact]
        public void GetUserById_WhenUserDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var userService = new UserService();

            // Act & Assert
            Assert.Throws<Exception>(() => userService.GetUserById("non-existent-id"));
        }
    }
}
