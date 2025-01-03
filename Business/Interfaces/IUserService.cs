using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Library.Models;

namespace Business_Library.Interfaces;

public interface IUserService
{
    UserBase GetUserById(string id);
    List<UserBase> GetAllUsers();
    bool AddUser(UserBase user);
    void RemoveUser(string id);
    bool UpdateUser(UserBase userBase);
}
