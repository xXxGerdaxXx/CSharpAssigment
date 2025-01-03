using Business_Library.Models;
using Business_Library.Helpers;

namespace Business_Library.Factories;

public static class UserFactory
{
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
