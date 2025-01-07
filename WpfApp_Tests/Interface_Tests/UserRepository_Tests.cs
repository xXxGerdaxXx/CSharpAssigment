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
    public void DeleteItemCommand_ShouldRemoveUserAndRefreshList()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var mockServiceProvider = new Mock<IServiceProvider>();
        var viewModel = new UserListViewModel(mockUserService.Object, mockServiceProvider.Object);

        // Ensure Users collection is initialized
        viewModel.Users = new ObservableCollection<UserBase>();

        var user = new UserBase { Id = "123", Name = "John" };
        viewModel.Users.Add(user);

        mockUserService.Setup(us => us.RemoveUser(user.Id)).Verifiable();

        // Act
        viewModel.DeleteItemCommand.Execute(user);

        // Assert
        mockUserService.Verify(us => us.RemoveUser(user.Id), Times.Once);
        Assert.DoesNotContain(user, viewModel.Users);
    }




}
