using Xunit;
using Business_Library.Repositories;
using Business_Library.Models;
using Business_Library.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Business_Library.Helpers;
using static Business_Library.Repositories.UserRepository;

namespace TestProject.Repositories;

public class UserRepositoryTests
{
    // Static instance of JsonSerializerOptions for reuse
    private static readonly JsonSerializerOptions JsonOptions = new ()
    {
        WriteIndented = true
    };

    [Fact]
    public void AddUser_ShouldAddUserAndSaveToFile()
    {
        try
        {
            // Arrange
            var userRepository = TestHelpers.CreateUserRepository();
            var user = new UserBase
            {
                Id = "123",
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com"
            };

            // Act
            bool result = userRepository.AddUser(user);
            var users = userRepository.GetAllUsers();

            // Assert
            Assert.True(result); // Ensure the user was successfully added
            Assert.Single(users); // Verify only one user exists
            Assert.Equal(user.Id, users[0].Id); // Verify user properties match
            Assert.Equal(user.Name, users[0].Name);
            Assert.Equal(user.Surname, users[0].Surname);
            Assert.Equal(user.Email, users[0].Email);
        }
        finally
        {
            TestHelpers.Cleanup(); // Cleanup test artifacts
        }
    }

    [Fact]
    public void GetUserById_ShouldReturnCorrectUser()
    {
        try
        {
            // Arrange
            var userRepository = TestHelpers.CreateUserRepository();
            var user = new UserBase
            {
                Id = "123",
                Name = "Alice",
                Surname = "Smith",
                Email = "alice.smith@example.com"
            };
            userRepository.AddUser(user);

            // Act
            var retrievedUser = userRepository.GetUserById("123");

            // Assert
            Assert.NotNull(retrievedUser); // Ensure the user exists
            Assert.Equal(user.Id, retrievedUser.Id);
            Assert.Equal(user.Name, retrievedUser.Name);
            Assert.Equal(user.Surname, retrievedUser.Surname);
            Assert.Equal(user.Email, retrievedUser.Email);
        }
        finally
        {
            TestHelpers.Cleanup();
        }
    }

    [Fact]
    public void GetUserById_WhenUserDoesNotExist_ShouldThrowUserNotFoundException()
    {
        try
        {
            // Arrange
            var userRepository = TestHelpers.CreateUserRepository();

            // Act & Assert
            var exception = Assert.Throws<UserNotFoundException>(() => userRepository.GetUserById("non-existent-id"));
            Assert.Equal("User with ID 'non-existent-id' not found.", exception.Message); // Ensure exception message matches
        }
        finally
        {
            TestHelpers.Cleanup();
        }
    }

    [Fact]
    public void AddDuplicateUser_ShouldNotAllowDuplicates()
    {
        try
        {
            // Arrange
            var userRepository = TestHelpers.CreateUserRepository();
            var user = new UserBase
            {
                Id = "123",
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com"
            };
            userRepository.AddUser(user);

            // Act
            bool result = userRepository.AddUser(user); // Try adding the same user again
            var users = userRepository.GetAllUsers();

            // Assert
            Assert.False(result); // Ensure duplicates are not allowed
            Assert.Single(users); // Verify only one user exists
        }
        finally
        {
            TestHelpers.Cleanup();
        }
    }

    [Fact]
    public void LoadUsersFromJson_ShouldLoadUsersFromFile()
    {
        try
        {
            // Arrange
            TestHelpers.Cleanup(); // Ensure a clean state
            Directory.CreateDirectory(TestHelpers.DirectoryPath); // Create directory for the test file

            var users = new List<UserBase>
            {
                new() { Id = "123", Name = "John", Surname = "Doe", Email = "john.doe@example.com" },
                new() { Id = "456", Name = "Jane", Surname = "Smith", Email = "jane.smith@example.com" }
            };

            // Serialize the users to JSON and write to the test file
            var json = JsonSerializer.Serialize(users, JsonOptions); // Use static JsonOptions
            File.WriteAllText(TestHelpers.FilePath, json); // Write serialized JSON to the test file

            var userRepository = TestHelpers.CreateUserRepository();

            // Act
            var loadedUsers = userRepository.GetAllUsers();

            // Assert
            Assert.Equal(users.Count, loadedUsers.Count); // Ensure all users are loaded
            for (int i = 0; i < users.Count; i++)
            {
                Assert.Equal(users[i].Id, loadedUsers[i].Id);
                Assert.Equal(users[i].Name, loadedUsers[i].Name);
                Assert.Equal(users[i].Surname, loadedUsers[i].Surname);
                Assert.Equal(users[i].Email, loadedUsers[i].Email);
            }
        }
        finally
        {
            TestHelpers.Cleanup(); // Ensure test artifacts are cleaned up
        }
    }
}
