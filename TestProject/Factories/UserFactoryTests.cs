using Xunit;
using Business_Library.Factories;
using Business_Library.Models;

namespace TestProject.Factories;

public class UserFactoryTests
{
    [Fact]
    public void CreateUser_ShouldReturnUserWithCorrectProperties()
    {
        // Arrange
        string name = "John";
        string surname = "Doe";
        string email = "john.doe@example.com";
        string mobile = "123456789";
        string address = "123 Main St";
        string postNumber = "12345";
        string city = "Springfield";

        // Act
        UserBase user = UserFactory.CreateUser(name, surname, email, mobile, address, postNumber, city);

        // Assert
        Assert.Equal(name, user.Name);
        Assert.Equal(surname, user.Surname);
        Assert.Equal(email, user.Email);
        Assert.Equal(mobile, user.Mobile);
        Assert.Equal(address, user.Address);
        Assert.Equal(postNumber, user.PostNumber);
        Assert.Equal(city, user.City);
        Assert.NotNull(user.Id); // Ensure ID is generated

        //It creates a UserBase object using the UserFactory.CreateUser method.
        //It validates that all properties of the created object match the input parameters.
        //It ensures the Id property is generated(by checking it's not null).
    }
}
