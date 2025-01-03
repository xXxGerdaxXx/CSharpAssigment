using Business_Library.Interfaces;
using Business_Library.Models;
using System.Text.Json;
using System.Diagnostics;

namespace Business_Library.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IFileService _fileService;
    private readonly string _filePath;
    private readonly List<UserBase> _users;

    // Constructor to inject IFileService and file path
    public UserRepository(IFileService fileService, string filePath)
    {
        _fileService = fileService;
        _filePath = filePath;
        _users = LoadUsersFromJson() ?? [];
    }

    // Add a user and save to file
    public bool AddUser(UserBase user)
    {
        if (_users.Any(u => u.Id == user.Id))
            return false; // this is needed in order to prevent duplicate users, don't remove it because the test associated with it will stop from working

        _users.Add(user);
        return SaveUsersToJson();
    }

    // Retrieve all users
    public List<UserBase> GetAllUsers()
    {
        return _users;
    }

    // Retrieve a user by ID
    public UserBase GetUserById(string id)
    {
        var user = _users.Find(u => u.Id == id);
        return user == null ? throw new Exception("User not found.") : user;
    }

    private bool SaveUsersToJson()
    {
        try
        {
            return _fileService.WriteToFile(_users); // Pass _users directly
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving users to file: {ex.Message}");
            return false;
        }
    }


    private List<UserBase>? LoadUsersFromJson()
    {
        try
        {
            if (!_fileService.FileExists())
                return null;

            var users = _fileService.ReadFromFile<UserBase>(); // Specify type
            return users.ToList(); // Ensure it returns a List<UserBase>
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading users from file: {ex.Message}");
            return null;
        }
    }
}
