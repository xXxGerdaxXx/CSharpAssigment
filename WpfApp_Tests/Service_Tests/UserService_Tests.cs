using Assignment_WpfApp.ViewModels;
using Business_Library.Interfaces;
using Business_Library.Models;
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
    [Fact]
    public void AddUser_ShouldAddUserSuccessfully_WhenValidUserIsProvided()
    {
        // Arrange
        var userService = new UserService(); 
        var user = new UserBase
        {
            Name = "John Doe",
            Email = "john.doe@example.com"
        };

        // Act
        var result = userService.AddUser(user);

        // Assert
        Assert.True(result); 
    }

}

