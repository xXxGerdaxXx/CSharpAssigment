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

    // Denna kod genererades med hjälp av ChatGPT
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


}

