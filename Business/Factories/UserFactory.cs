using Business_Library.Models;
using Business_Library.Helpers;

namespace Business_Library.Factories;

/// <summary>
/// Factory class for creating instances of <see cref="UserBase"/>.
/// </summary>
public static class UserFactory
{
    /// <summary>
    /// Creates a new <see cref="UserBase"/> object with the specified user details.
    /// Automatically generates a unique ID for the user.
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="surname">last name</param>
    /// <param name="email">email</param>
    /// <param name="mobile">phone number</param>
    /// <param name="address">address</param>
    /// <param name="postNumber">postal code</param>
    /// <param name="city">city</param>
    /// <returns>A <see cref="UserBase"/> instance populated with user details and a unique ID.</returns>

    public static UserBase CreateUser(
        string name,
        string surname,
        string email,
        string mobile,
        string address,
        string postNumber,
        string city)
    {
        return new UserBase
        {
            Id = IdGenerator.GenerateShortGuid(6),
            Name = name,
            Surname = surname,
            Email = email,
            Mobile = mobile,
            Address = address,
            PostNumber = postNumber,
            City = city
        };
    }
}
