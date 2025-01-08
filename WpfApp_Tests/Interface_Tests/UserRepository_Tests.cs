using Business_Library.Interfaces;
using Business_Library.Models;
using Business_Library.Repositories;
using Moq;
using Business_Library.Services;
using Assignment_WpfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_WpfApp.ViewModels;
using System.Collections.ObjectModel;

namespace WpfApp_Tests.Interface_Tests;

public class UserRepository_Tests
{
    // Denna kod genererades med hjälp av ChatGPT

    [Fact]
    public void AddUser_ShouldCallAddUser_WhenUserIsValid()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        var repository = new UserRepository(mockFileService.Object, "path/to/file.json");

        var user = new UserBase { Id = "123", Name = "John", Email = "john.doe@example.com" };

        // Mock FileService behavior for saving
        mockFileService.Setup(fs => fs.WriteToFile(It.IsAny<List<UserBase>>())).Returns(true);

        // Act
        var result = repository.AddUser(user);

        // Assert
        Assert.True(result);
        var allUsers = repository.GetAllUsers();
        Assert.Contains(user, allUsers); // Ensure the user was added
    }

    [Fact]
    public void AddUser_ShouldCallWriteToFile_WhenUserIsAdded()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        var repository = new UserRepository(mockFileService.Object, "path/to/file.json");

        var user = new UserBase { Id = "123", Name = "John", Email = "john.doe@example.com" };

        mockFileService.Setup(fs => fs.WriteToFile(It.IsAny<List<UserBase>>())).Returns(true);

        // Act
        var result = repository.AddUser(user);

        // Assert
        Assert.True(result);
        mockFileService.Verify(fs => fs.WriteToFile(It.IsAny<List<UserBase>>()), Times.Once);
    }



    [Fact]
    public void DeleteItemCommand_ShouldRemoveUserFromUsersCollection()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var user = new UserBase { Id = "123", Name = "John" };

        // Simulate an in-memory list for the mock
        var users = new List<UserBase> { user };
        mockUserService.Setup(us => us.GetAllUsers()).Returns(() => users);
        mockUserService.Setup(us => us.RemoveUser(It.IsAny<string>()))
                       .Callback<string>(id => users.RemoveAll(u => u.Id == id));

        var viewModel = new UserListViewModel(mockUserService.Object, null!);
        viewModel.RefreshUsers(); // Ensure the Users collection is initialized

        // Act
        viewModel.DeleteItemCommand.Execute(user);

        // Assert
        Assert.Empty(viewModel.Users); // Ensure user was removed
        mockUserService.Verify(us => us.RemoveUser("123"), Times.Once);
    }






}
