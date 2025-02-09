﻿using Xunit;
using Business_Library.Models;
using Business_Library.Factories;

namespace TestProject.Factories;

public class UserFactory_Tests
{

    /// I used ChatGPT and tips and tricks video for these tests
    /// <summary>
    /// Tests that <see cref="UserFactory.CreateUser"/> method creates a valid <see cref="UserBase"/> object 
    /// Ensures that the generated user has a unique ID of the correct length.
    /// </summary>
    [Fact]
    public void CreateUser_ShouldReturnUserBase()
    {
        // Arrange
        string name = "John";
        string surname = "Doe";
        string email = "john.doe@example.com";
        string mobile = "1234567890";
        string address = "123 Elm Street";
        string postNumber = "12345";
        string city = "Sample City";

        // Act
        var user = UserFactory.CreateUser(name, surname, email, mobile, address, postNumber, city);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(name, user.Name);
        Assert.Equal(surname, user.Surname);
        Assert.Equal(email, user.Email);
        Assert.Equal(mobile, user.Mobile);
        Assert.Equal(address, user.Address);
        Assert.Equal(postNumber, user.PostNumber);
        Assert.Equal(city, user.City);
        Assert.NotNull(user.Id);
        Assert.Equal(6, user.Id.Length); 
    }


    /// <summary>
    /// Tests that <see cref="UserFactory.CreateUser"/> method generates unique IDs
    /// for each created <see cref="UserBase"/> object, even when the input parameters are identical
    /// Ensures that the ID generation logic works as intended to avoid duplicate IDs
    /// </summary>
    [Fact]
    public void CreateUser_ShouldGenerateUniqueIds()
    {
        // Arrange
        string name = "John";
        string surname = "Doe";
        string email = "john.doe@example.com";
        string mobile = "1234567890";
        string address = "123 Elm Street";
        string postNumber = "12345";
        string city = "Sample City";

        // Act
        var user1 = UserFactory.CreateUser(name, surname, email, mobile, address, postNumber, city);
        var user2 = UserFactory.CreateUser(name, surname, email, mobile, address, postNumber, city);

        // Assert
        Assert.NotEqual(user1.Id, user2.Id); 
    }
}
