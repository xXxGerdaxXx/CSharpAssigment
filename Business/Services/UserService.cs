using Business_Library.Helpers;
using Business_Library.Interfaces;
using Business_Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Business_Library.Services;

/// <summary>
/// Manages a collection of users allowing for adding new users, 
/// retrieving user details, updating user informatio and deleting users.
/// User data is stored in a JSON file using file service.
/// </summary>

public class UserService : IUserService
{
    private readonly List<UserBase> _users;
    private readonly IFileService _fileService;
    private readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "applicationUserList.json");

    public UserService(IFileService fileService)
    {
        _fileService = fileService;
        _users = _fileService.ReadFromFile<UserBase>()?.ToList() ?? [];

    }

    // Retrieve a user by ID
    public UserBase GetUserById(string id)
    {
        var user = _users.Find(u => u.Id == id);
        return user ?? throw new Exception("User not found.");
    }


    public List<UserBase> GetAllUsers() => _users;

    public bool AddUser(UserBase user)
    {
        try
        {
            user.Id = Guid.NewGuid().ToString(); 
            _users.Add(user); 
            SaveUsersToJson(); 
            return _fileService.WriteToFile(_users);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding user: {ex.Message}");
            return false; 
        }
    }

    // Remove a user by ID
    public void RemoveUser(string id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user == null)
        {
            Console.WriteLine($"User with ID {id} not found.");
            return;
        }

        _users.Remove(user);
        _fileService.WriteToFile(_users);
        Console.WriteLine($"User with ID {id} has been removed.");
    }

    // Update user details
    public bool UpdateUser(UserBase updatedUser)
    {
        var user = _users.Find(u => u.Id == updatedUser.Id);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return false;
        }

        // Update user details
        user.Name = updatedUser.Name;
        user.Surname = updatedUser.Surname;
        user.Email = updatedUser.Email;
        user.Mobile = updatedUser.Mobile;
        user.Address = updatedUser.Address;
        user.PostNumber = updatedUser.PostNumber;
        user.City = updatedUser.City;

        return _fileService.WriteToFile(_users);
    }

    // Save users to JSON file
    private void SaveUsersToJson()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving users to file: {ex.Message}");
        }
    }


}
