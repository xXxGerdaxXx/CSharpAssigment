using Business_Library.Models;
using System.Collections.Generic;

namespace Business_Library.Interfaces;

/// <summary>
/// Defines the contract for validating user input so that required fields are not left empty.
/// </summary>
public interface IValidationService
{
    /// <summary>
    /// Validates the given user and returns a dictionary of validation errors.
    /// </summary>
    /// <param name="user">The user to validate.</param>
    /// <returns>A dictionary where the key is the field name and the value is the error message.</returns>
    Dictionary<string, string> ValidateUser(UserBase user);
}
