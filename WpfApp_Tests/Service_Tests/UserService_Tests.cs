using Assignment_WpfApp.ViewModels;
using Business_Library.Interfaces;
using Business_Library.Models;
using Business_Library.Repositories;
using Business_Library.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Tests.Service_Tests;

public class UserService_Tests
{

    /// I have used ChatGPT 4o to generate these tests

    /// <summary>
    /// Tests that a valid user is successfully added using the AddUser method.
    /// Ensures that the method returns true and calls the WriteToFile function to save the user data.
    /// </summary>

    [Fact]
    public void AddUser_ShouldAddUserSuccessfully_WhenValidUserIsProvided()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(fs => fs.ReadFromFile<UserBase>()).Returns([]); // Simulate an empty user list
        mockFileService.Setup(fs => fs.WriteToFile(It.IsAny<List<UserBase>>())).Returns(true); // Simulate a successful save

        var userService = new UserService(mockFileService.Object); // Inject the mock file service

        var user = new UserBase
        {
            Name = "John Doe",
            Email = "john.doe@example.com"
        };

        // Act
        var result = userService.AddUser(user);

        // Assert
        Assert.True(result); // Verify that AddUser returns true
        mockFileService.Verify(fs => fs.WriteToFile(It.IsAny<List<UserBase>>()), Times.Once); // Verify WriteToFile is called
    }



    /// <summary>
    /// Ensures that the AddUser method calls the WriteToFile method when a user is successfully added.
    /// Verifies that WriteToFile is called exactly once during the process.
    /// </summary>
    /// 
    [Fact]
    public void AddUser_ShouldCallWriteToFile_WhenUserIsAdded()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(fs => fs.ReadFromFile<UserBase>()).Returns([]); // Simulate an empty list initially
        mockFileService.Setup(fs => fs.WriteToFile(It.IsAny<List<UserBase>>())).Returns(true); // Simulate successful file write

        var userService = new UserService(mockFileService.Object);
        var user = new UserBase { Name = "John Doe", Email = "john.doe@example.com" };

        // Act
        var result = userService.AddUser(user);

        // Assert
        Assert.True(result); // Ensure AddUser returns true
        mockFileService.Verify(fs => fs.WriteToFile(It.IsAny<List<UserBase>>()), Times.Once); // Ensure WriteToFile is called once
    }


    /// <summary>
    /// Verifies that AddUser fails and returns false when a user with empty required fields is provided.
    /// Ensures the repository does not allow invalid users with empty fields to be added.
    /// </summary>
    [Fact]
    public void AddUser_ShouldFail_WhenUserWithEmptyFields()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        var userRepository = new UserRepository(mockFileService.Object, "testUsers.json");
        var invalidUser = new UserBase
        {
            Name = "", // Empty name
            Surname = "", // Empty surname
            Email = "", // Empty email
            Mobile = "" // Empty mobile
        };

        // Act
        var result = userRepository.AddUser(invalidUser);

        // Assert
        Assert.False(result, "AddUser should return false when the user has empty required fields.");
    }

    /// <summary>
    /// Ensures that the AddUser method returns false when a null user object is passed.
    /// Validates that the repository gracefully handles null input and prevents it from being added.
    /// </summary>
    [Fact]
    public void AddUser_ShouldFail_WhenUserIsNull()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        var userRepository = new UserRepository(mockFileService.Object, "testUsers.json");

        // Act
        var result = userRepository.AddUser(null!);

        // Assert
        Assert.False(result, "AddUser should return false when the user is null.");
    }

}

