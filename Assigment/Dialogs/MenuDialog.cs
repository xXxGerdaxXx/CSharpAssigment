﻿using Business_Library.Factories;
using Business_Library.Interfaces;
using Business_Library.Models;


namespace Assigment.Dialogs;

public class MenuDialog(IUserService userService)
{
    private readonly IUserService _userService = userService;

    public void MainMenu()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("\nUser Management System");
            Console.WriteLine("********************************");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. List All Users");
            Console.WriteLine("3. Search User by ID");
            Console.WriteLine("4. Exit");
            Console.WriteLine("********************************");
            Console.Write("Choose an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddUser();
                    break;
                case "2":
                    ListAllUsers();
                    break;
                case "3":
                    SearchUserById();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Goodbye!");
                    running = false; 
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to return to the menu...");
                Console.ReadLine();
            }
        }
    }

    private void AddUser()
    {
        Console.Clear();
        Console.WriteLine("\nAdd User:");

        string name;
        do
        {
            Console.Write("Enter Name: ");
            name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Please try again.");
            }
        } while (string.IsNullOrWhiteSpace(name));
        Console.Clear();

        string surname;
        do
        {
            Console.Write("Enter Surname: ");
            surname = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(surname))
            {
                Console.WriteLine("Surname cannot be empty. Please try again.");
            }
        } while (string.IsNullOrWhiteSpace(surname));
        Console.Clear();

        string email;
        do
        {
            Console.Write("Enter Email: ");
            email = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Invalid email format. Please try again.");
            }
        } while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"));
        Console.Clear();

        string mobile;
        do
        {
            Console.Write("Enter Mobile: ");
            mobile = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(mobile) || !long.TryParse(mobile, out _))
            {
                Console.WriteLine("Invalid mobile number. Please enter digits only.");
            }
        } while (string.IsNullOrWhiteSpace(mobile) || !long.TryParse(mobile, out _));
        Console.Clear();

        string address;
        do
        {
            Console.Write("Enter Address: ");
            address = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("Address cannot be empty. Please try again.");
            }
        } while (string.IsNullOrWhiteSpace(address));
        Console.Clear();

        string postNumber;
        do
        {
            Console.Write("Post Number: ");
            postNumber = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(postNumber))
            {
                Console.WriteLine("Post number cannot be empty. Please try again.");
            }
        } while (string.IsNullOrWhiteSpace(postNumber));
        Console.Clear();

        string city;
        do
        {
            Console.Write("City: ");
            city = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(city))
            {
                Console.WriteLine("City cannot be empty. Please try again.");
            }
        } while (string.IsNullOrWhiteSpace(city));
        Console.Clear();

        ///Added success/failure check after calling the _userService.AddUser(user) after watching Hans's tips video and comparing my code
        try
        {
            var user = UserFactory.CreateUser(name, surname, email, mobile, address, postNumber, city);

            /// Call the AddUser method and store the result
            bool result = _userService.AddUser(user);

            /// Check if the user was successfully added
            if (result)
            {
                Console.WriteLine("\nUser successfully registered!");
            }
            else
            {
                Console.WriteLine("\nUser could not be registered.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError registering user: {ex.Message}");
        }
    }


    private void ListAllUsers()
    {
        Console.Clear();
        Console.WriteLine("\nList of All Users:");

        List<UserBase> users = _userService.GetAllUsers();

        if (users.Count == 0)
        {
            Console.WriteLine("No users found.");
            return;
        }

        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Surname: {user.Surname}, Email: {user.Email}, Mobile: {user.Mobile}, Address: {user.Address}, Post Number: {user.PostNumber}, City: {user.City}");
        }
    }

    private void SearchUserById()
    {
        Console.Clear();
        Console.WriteLine("\nSearch User by ID:");

        Console.Write("Enter User ID to search: ");
        string idInput = Console.ReadLine() ?? string.Empty;

        try
        {
            var user = _userService.GetUserById(idInput);
            Console.WriteLine($"\nUser found - ID: {user.Id}, Name: {user.Name}, Surname: {user.Surname}, Email: {user.Email}, Mobile: {user.Mobile}, Address: {user.Address}, Post Number: {user.PostNumber}, City: {user.City}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}