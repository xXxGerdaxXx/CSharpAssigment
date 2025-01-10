using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Business_Library.Interfaces;
using Business_Library.Models;

namespace Business_Library.Services;

/// <summary>
/// Implements validation logic for user input, ensuring required fields are not left empty and values are in correct formats.
/// </summary>
public class ValidationService : IValidationService
{
    public Dictionary<string, string> ValidateUser(UserBase user)
    {
        var errors = new Dictionary<string, string>();

        if (user == null)
        {
            errors["General"] = "User cannot be null.";
            return errors;
        }

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
        }

        if (string.IsNullOrWhiteSpace(user.Mobile))
        {
            errors[nameof(user.Mobile)] = "Mobile number is required.";
        }
        else if (!Regex.IsMatch(user.Mobile, @"^\+?[0-9\s-]+$"))
        {
            errors[nameof(user.Mobile)] = "Mobile number must contain only valid characters.";
        }

        if (string.IsNullOrWhiteSpace(user.Address))
            errors[nameof(user.Address)] = "Address is required.";

        if (string.IsNullOrWhiteSpace(user.PostNumber))
            errors[nameof(user.PostNumber)] = "Post number is required.";

        if (string.IsNullOrWhiteSpace(user.City))
            errors[nameof(user.City)] = "City is required.";

        return errors;
    }

    private static bool IsValidEmail(string email)
    {
        var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailRegex);
    }
}
