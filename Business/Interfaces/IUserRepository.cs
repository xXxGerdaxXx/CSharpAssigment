using Business_Library.Models;

namespace Business_Library.Interfaces;

/// <summary>
/// Defines the contract for user repository (adding users, retrieving all users, getting user by their id
/// </summary>
public interface IUserRepository
{
    bool AddUser(UserBase user);
    List<UserBase> GetAllUsers();
    UserBase GetUserById(string id);
}
