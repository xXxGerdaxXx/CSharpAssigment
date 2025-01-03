using Business_Library.Models;

namespace Business_Library.Interfaces;

public interface IUserRepository
{
    bool AddUser(UserBase user);
    List<UserBase> GetAllUsers();
    UserBase GetUserById(string id);
}
