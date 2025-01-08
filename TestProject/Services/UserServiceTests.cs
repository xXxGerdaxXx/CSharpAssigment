using Moq;
using Xunit;
using Business_Library.Models;
using Business_Library.Services;
using Business_Library.Interfaces;
using System.Collections.Generic;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ShouldAddUserSuccessfully()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();

        // Simulate an empty user list initially
        mockFileService.Setup(fs => fs.ReadFromFile<UserBase>()).Returns(new List<UserBase>());
        // Simulate successful file writing
        mockFileService.Setup(fs => fs.WriteToFile(It.IsAny<List<UserBase>>())).Returns(true);

        var userService = new UserService(mockFileService.Object);

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

        // Verify that WriteToFile was called exactly once
        mockFileService.Verify(fs => fs.WriteToFile(It.IsAny<List<UserBase>>()), Times.Once);
    }

    [Fact]
    public void GetUserById_ShouldReturnCorrectUser()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();

        // Simulate an empty user list initially
        var users = new List<UserBase>();
        mockFileService.Setup(fs => fs.ReadFromFile<UserBase>()).Returns(users);
        mockFileService.Setup(fs => fs.WriteToFile(It.IsAny<List<UserBase>>())).Returns(true);

        var userService = new UserService(mockFileService.Object);

        var user = new UserBase
        {
            Name = "Alice",
            Surname = "Smith",
            Email = "alice.smith@example.com"
        };

        userService.AddUser(user);
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
        var mockFileService = new Mock<IFileService>();

        // Simulate an empty user list
        mockFileService.Setup(fs => fs.ReadFromFile<UserBase>()).Returns(new List<UserBase>());

        var userService = new UserService(mockFileService.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => userService.GetUserById("non-existent-id"));
    }
}
