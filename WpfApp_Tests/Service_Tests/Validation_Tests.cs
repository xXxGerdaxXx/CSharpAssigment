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

public class Validation_Tests
{

    // Denna kod genererades med hjälp av ChatGPT
    [Fact]
    public void ValidateUser_ShouldReturnErrors_ForInvalidUser()
    {
        // Arrange
        var mockValidationService = new Mock<IValidationService>();
        var invalidUser = new UserBase { Id = "123", Name = "", Email = "invalid-email" };

        mockValidationService.Setup(vs => vs.ValidateUser(invalidUser))
                             .Returns(new Dictionary<string, string>
                             {
                             { "Name", "Name is required" },
                             { "Email", "Invalid email format" }
                             });

        // Act
        var errors = mockValidationService.Object.ValidateUser(invalidUser);

        // Assert
        Assert.NotEmpty(errors);
        Assert.Equal("Name is required", errors["Name"]);
        Assert.Equal("Invalid email format", errors["Email"]);
    }

    [Fact]
    public void ValidateUser_ShouldPass_ForValidUser()
    {
        // Arrange
        var validationService = new ValidationService();
        var validUser = new UserBase
        {
            Name = "John",
            Surname = "Doe",
            Email = "john.doe@example.com",
            Mobile = "+1234567890",
            Address = "123 Main Street",
            PostNumber = "12345",
            City = "New York"
        };

        // Act
        Dictionary<string, string> errors = validationService.ValidateUser(validUser);

        // Assert
        Assert.Empty(errors); // Assert that no errors are returned for a valid user
    }

}
