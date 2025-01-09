using Xunit;
using Business_Library.Repositories;
using Business_Library.Models;
using Business_Library.Services;
using System.Collections.Generic;
using System.IO;
using static Business_Library.Repositories.UserRepository;

namespace TestProject.Repositories;

public class UserRepositoryTests
{
    // Constants for the test data directory and file name
    private const string DirectoryPath = "TestData";
    private const string FileName = "testUsers.json";

    // Full file path for the test data
    private string FilePath => Path.Combine(DirectoryPath, FileName);

    // Helper method to create a UserRepository instance with a FileService
    private UserRepository CreateUserRepository()
    {
        var fileService = new FileService(DirectoryPath, FileName);
        return new UserRepository(fileService, FilePath);
    }

    // Cleanup method to ensure test artifacts are removed
    private void Cleanup()
    {
        try
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            if (Directory.Exists(DirectoryPath))
                Directory.Delete(DirectoryPath, true); // Force delete directory
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    [Fact]
    public void AddUser_ShouldAddUserAndSaveToFile()
    {
        try
        {
            // Arrange
            var userRepository = CreateUserRepository();
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
            Cleanup(); // Cleanup test artifacts
        }
    }

    [Fact]
    public void GetUserById_ShouldReturnCorrectUser()
    {
        try
        {
            // Arrange
            var userRepository = CreateUserRepository();
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
            Cleanup();
        }
    }

    [Fact]
    public void GetUserById_WhenUserDoesNotExist_ShouldThrowUserNotFoundException()
    {
        try
        {
            // Arrange
            var userRepository = CreateUserRepository();

            // Act & Assert
            var exception = Assert.Throws<UserNotFoundException>(() => userRepository.GetUserById("non-existent-id"));
            Assert.Equal("User with ID 'non-existent-id' not found.", exception.Message); // Ensure exception message matches
        }
        finally
        {
            Cleanup();
        }
    }


    [Fact]
    public void AddDuplicateUser_ShouldNotAllowDuplicates()
    {
        try
        {
            // Arrange
            var userRepository = CreateUserRepository();
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
            Cleanup();
        }
    }

    [Fact]
    public void LoadUsersFromJson_ShouldLoadUsersFromFile()
    {
        try
        {
            // Arrange
            Directory.CreateDirectory(DirectoryPath);
            var users = new List<UserBase>
            {
                new () { Id = "123", Name = "John", Surname = "Doe", Email = "john.doe@example.com" },
                new () { Id = "456", Name = "Jane", Surname = "Smith", Email = "jane.smith@example.com" }
            };

            // Serialize the users to JSON and write to the test file
            var json = System.Text.Json.JsonSerializer.Serialize(users, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);

            var userRepository = CreateUserRepository();

            // Act
            var loadedUsers = userRepository.GetAllUsers();

            // Assert
            Assert.Equal(2, loadedUsers.Count); // Ensure all users are loaded
            Assert.Equal("John", loadedUsers[0].Name);
            Assert.Equal("Doe", loadedUsers[0].Surname);
            Assert.Equal("Jane", loadedUsers[1].Name);
            Assert.Equal("Smith", loadedUsers[1].Surname);
        }
        finally
        {
            Cleanup();
        }
    }



}
