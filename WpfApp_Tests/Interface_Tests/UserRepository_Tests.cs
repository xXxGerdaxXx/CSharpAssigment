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
    /// I used ChatGPT4o to generate these tests

    /// <summary>
    /// Verifies that the `AddUser` method of `UserRepository` correctly adds a valid user
    /// to the repository and that the user appears in the list of all users.
    /// </summary>
    [Fact]
    public void AddUser_ShouldCallAddUser_WhenUserIsValid()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>(); // Mocked FileService
        var repository = new UserRepository(mockFileService.Object, "path/to/file.json");
        var user = new UserBase { Id = "123", Name = "John", Email = "john.doe@example.com" };

        mockFileService.Setup(fs => fs.WriteToFile(It.IsAny<List<UserBase>>())).Returns(true); // Simulate successful file save

        // Act
        var result = repository.AddUser(user); // Add the user

        // Assert
        Assert.True(result); // Ensure AddUser returns true
        var allUsers = repository.GetAllUsers(); // Get all users
        Assert.Contains(user, allUsers); // Verify the user is in the list
    }


    /// <summary>
    /// Ensures that the `AddUser` method of `UserRepository` calls the `WriteToFile` method
    /// of the `IFileService` to persist the user data.
    /// </summary>
    [Fact]
    public void AddUser_ShouldCallWriteToFile_WhenUserIsAdded()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>(); // Mocked FileService
        var repository = new UserRepository(mockFileService.Object, "path/to/file.json");
        var user = new UserBase { Id = "123", Name = "John", Email = "john.doe@example.com" };

        mockFileService.Setup(fs => fs.WriteToFile(It.IsAny<List<UserBase>>())).Returns(true); // Simulate successful file save

        // Act
        var result = repository.AddUser(user); // Add the user

        // Assert
        Assert.True(result); // Ensure AddUser returns true
        mockFileService.Verify(fs => fs.WriteToFile(It.IsAny<List<UserBase>>()), Times.Once); // Verify WriteToFile was called exactly once
    }




    /// <summary>
    /// Verifies that the `DeleteItemCommand` in the `UserListViewModel` removes a user
    /// from the `Users` collection and calls the `RemoveUser` method in `IUserService`.
    /// </summary>
    [Fact]
    public void DeleteItemCommand_ShouldRemoveUserFromUsersCollection()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>(); // Mocked UserService
        var user = new UserBase { Id = "123", Name = "John" };

        var users = new List<UserBase> { user }; // Simulate an in-memory list of users
        mockUserService.Setup(us => us.GetAllUsers()).Returns(() => users); // Mock user retrieval
        mockUserService.Setup(us => us.RemoveUser(It.IsAny<string>()))
                       .Callback<string>(id => users.RemoveAll(u => u.Id == id)); // Simulate user removal

        var viewModel = new UserListViewModel(mockUserService.Object, null!); // Initialize the ViewModel
        viewModel.RefreshUsers(); // Ensure the Users collection is populated

        // Act
        viewModel.DeleteItemCommand.Execute(user); // Execute delete command

        // Assert
        Assert.Empty(viewModel.Users); // Verify the user is removed from the Users collection
        mockUserService.Verify(us => us.RemoveUser("123"), Times.Once); // Verify RemoveUser was called exactly once
    }

}
