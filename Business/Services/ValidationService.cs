using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Business_Library.Interfaces;
using Business_Library.Models;

namespace Business_Library.Services;


/// <summary>
/// This part of the code I used ChatGPT4o to generate validation for me
/// Validates required fields and checks the format of email and mobile numbers
/// </summary>
public class ValidationService : IValidationService
{
    public Dictionary<string, string> ValidateUser(UserBase user)
    {
        var errors = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(user.Name))
            errors[nameof(user.Name)] = "Name is required.";

        if (string.IsNullOrWhiteSpace(user.Surname))
            errors[nameof(user.Surname)] = "Surname is required.";

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            errors[nameof(user.Email)] = "Email is required.";
        }
        else if (!IsValidEmail(user.Email))
        {
            
            errors[nameof(user.Email)] = "Invalid email format.";
            Console.WriteLine($"Invalid Email Error Added: {user.Email}");
        }

        if (string.IsNullOrWhiteSpace(user.Mobile))
        {
            errors[nameof(user.Mobile)] = "Mobile number is required.";
        }
        else if (!Regex.IsMatch(user.Mobile, @"^\d+$"))
        {
            errors[nameof(user.Mobile)] = "Mobile number must contain only digits.";
        }

        return errors;
    }



    private static bool IsValidEmail(string email)
    {
        var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var isValid = Regex.IsMatch(email, emailRegex);
        Console.WriteLine($"Email: {email}, IsValid: {isValid}");
        return isValid;

    }
}
