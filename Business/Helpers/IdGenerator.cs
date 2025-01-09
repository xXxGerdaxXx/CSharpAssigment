using System;

namespace Business_Library.Helpers;

/// <summary>
/// I used chat to generate the idgenerator for me
/// Provides helper methods for generating unique ids.
/// </summary>
public static class IdGenerator
{
    /// <summary>
    /// Generates a short, unique id.
    /// </summary>
    /// <param name="length">Length of the id, between 1-32 char.</param>
    /// <returns>A string containing a unique id of 6 characters</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when length is less than 1 or more than 32
    /// </exception>
    public static string GenerateShortGuid(int length = 6)
    {
        if (length < 1 || length > 32)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be between 1 and 32.");
        }

        return Guid.NewGuid().ToString("N").Substring(0, length);
    }
}
