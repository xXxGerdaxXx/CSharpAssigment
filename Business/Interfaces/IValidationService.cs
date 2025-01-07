using Business_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Library.Interfaces;

public interface IValidationService
{
    Dictionary<string, string> ValidateUser(UserBase user);
}

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
            errors[nameof(user.Email)] = "Email is required.";

        if (string.IsNullOrWhiteSpace(user.Mobile))
            errors[nameof(user.Mobile)] = "Mobile is required.";


        return errors;
    }
}
