﻿using Business_Library.Helpers;
using Business_Library.Interfaces;
using Business_Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Business_Library.Services;

public class UserService : IUserService
{
    private readonly List<UserBase> _users;
    private readonly IFileService _fileService;
    private readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "applicationUserList.json");

    public UserService(IFileService fileService)
    {
        _fileService = fileService;
        _users = _fileService.ReadFromFile<UserBase>()?.ToList() ?? new List<UserBase>();

    }

    // Retrieve a user by ID
    public UserBase GetUserById(string id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user == null)
            throw new Exception("User not found.");
        return user;
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

    // Load users from JSON file
    private List<UserBase>? LoadUsersFromJson()
    {
        try
        {
            if (!File.Exists(FilePath))
                return null;

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<UserBase>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users from file: {ex.Message}");
            return null;
        }
    }
}
