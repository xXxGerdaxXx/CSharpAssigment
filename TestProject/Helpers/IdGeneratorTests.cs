using Xunit;
using Business_Library.Helpers;
using System;
using System.Collections.Generic;

namespace TestProject.Helpers;

/// <summary>
/// I used Hans's tips & tricks video & chat to write the tests
/// Tests for the IdGenerator class to ensure correct behavior of the GenerateShortGuid method.
/// </summary>
public class IdGeneratorTests
{
    //[Fact]
    
    //public void Generate_ShoudlReturnGuidAsString()
    //{
    //    // act
    //    var result = IdGenerator.GenerateShortGuid();

    //    // assert
    //    Assert.NotEmpty(result);
    //    Assert.True(Guid.TryParse(result, out _));
    // If I understand right, this test doesn't work in my case because i shortened my guid to 6 digits 
    //



    /// <summary>
    /// Verifies that GenerateShortGuid produces a string of the correct length for valid inputs.
    /// </summary>
    /// <param name="length">The desired length of the generated ID.</param>
    [Theory]
    [InlineData(6)]   // Test with a length of 6
    [InlineData(16)]  // Test with a length of 16
    [InlineData(32)]  // Test with a length of 32 (max allowed length)
    public void GenerateShortGuid_ShouldReturnCorrectLength(int length)
    {
        // Act
        string result = IdGenerator.GenerateShortGuid(length);

        // Assert
        Assert.Equal(length, result.Length);
    }

    /// <summary>
    /// Verifies that GenerateShortGuid throws an ArgumentOutOfRangeException for invalid lengths.
    /// </summary>
    /// <param name="length">The invalid length to test.</param>
    [Theory]
    [InlineData(0)]    // Test with length 0 (invalid)
    [InlineData(33)]   // Test with length 33 (greater than the max allowed 32)
    public void GenerateShortGuid_InvalidLength_ShouldThrowArgumentOutOfRangeException(int length)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => IdGenerator.GenerateShortGuid(length));
    }

    /// <summary>
    /// Verifies that GenerateShortGuid produces unique IDs for multiple calls.
    /// </summary>
    [Fact]
    public void GenerateShortGuid_ShouldReturnUniqueIds()
    {
        // Arrange
        var idSet = new HashSet<string>(); // To track generated IDs and prevent duplicates

        // Act
        for (int i = 0; i < 1000; i++)
        {
            string newId = IdGenerator.GenerateShortGuid(8);

            // Assert
            Assert.True(idSet.Add(newId), $"Duplicate ID found: {newId}"); // Ensure IDs are unique
        }
    }
}
