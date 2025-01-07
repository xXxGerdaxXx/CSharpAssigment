using System;

namespace Business_Library.Helpers;

public static class IdGenerator
{
    public static string GenerateShortGuid(int length = 6)
    {
        if (length < 1 || length > 32)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be between 1 and 32.");
        }

        return Guid.NewGuid().ToString("N").Substring(0, length);
    }
}
