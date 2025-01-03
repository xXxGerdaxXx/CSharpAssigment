using Xunit;
using Business_Library.Helpers;
using System;
using System.Collections.Generic;

namespace TestProject.Helpers
{
    public class IdGeneratorTests
    {
        // -----------------------------------------------
        // Test Case 1: Ensure GenerateShortGuid returns a string of the correct length
        // -----------------------------------------------

        /// <summary>
        /// This test verifies that the generated ID has the correct length for valid inputs.
        /// </summary>
        /// <param name="length">The desired length of the generated ID.</param>
        [Theory]                      // Indicates a parameterized test case
        [InlineData(6)]               // Test with length 6
        [InlineData(16)]              // Test with length 16
        [InlineData(32)]              // Test with length 32 (max allowed length)
        public void GenerateShortGuid_ShouldReturnCorrectLength(int length)
        {
            // Act: Generate a short GUID with the specified length
            string result = IdGenerator.GenerateShortGuid(length);

            // Assert: Check if the length of the result matches the expected length
            Assert.Equal(length, result.Length);
        }

        // -----------------------------------------------
        // Test Case 2: Ensure GenerateShortGuid throws an exception for invalid lengths
        // -----------------------------------------------

        /// <summary>
        /// This test verifies that an ArgumentOutOfRangeException is thrown for invalid lengths.
        /// </summary>
        /// <param name="length">The invalid length to test.</param>
        [Theory]                      // Indicates a parameterized test case
        [InlineData(0)]               // Test with length 0 (invalid)
        [InlineData(33)]              // Test with length 33 (greater than the max allowed 32)
        public void GenerateShortGuid_InvalidLength_ShouldThrowArgumentOutOfRangeException(int length)
        {
            // Act & Assert: Verify that an ArgumentOutOfRangeException is thrown
            // when GenerateShortGuid is called with an invalid length
            Assert.Throws<ArgumentOutOfRangeException>(() => IdGenerator.GenerateShortGuid(length));
        }

        // -----------------------------------------------
        // Test Case 3: Ensure GenerateShortGuid returns unique IDs
        // -----------------------------------------------

        /// <summary>
        /// This test verifies that multiple calls to GenerateShortGuid produce unique IDs.
        /// </summary>
        [Fact]                        // Indicates a single test case (not parameterized)
        public void GenerateShortGuid_ShouldReturnUniqueIds()
        {
            // Arrange: Create a HashSet to store generated IDs (HashSet prevents duplicates)
            var idSet = new HashSet<string>();

            // Act: Generate 1000 short GUIDs with a length of 8 characters
            for (int i = 0; i < 1000; i++)
            {
                string newId = IdGenerator.GenerateShortGuid(8);

                // Check if the new ID is unique and add it to the HashSet
                bool isUnique = idSet.Add(newId);

                // Assert: Ensure the new ID is unique (HashSet.Add returns false if a duplicate is added)
                Assert.True(isUnique, $"Duplicate ID found: {newId}");
            }
        }
    }
}
