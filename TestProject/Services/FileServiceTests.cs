using Xunit;
using Business_Library.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TestProject.Services;

public class FileServiceTests
{
    [Fact]
    public void WriteToFile_ShouldCreateFileWithContent()
    {
        // Arrange
        string directoryPath = Path.Combine(Path.GetTempPath(), "TestData");
        string fileName = "testfile.json";
        string filePath = Path.Combine(directoryPath, fileName);
        var data = new List<string> { "Hello", "World" };

        // Ensure the directory is clean before starting
        if (Directory.Exists(directoryPath))
        {
            Directory.Delete(directoryPath, true);
        }

        var fileService = new FileService(directoryPath, fileName);

        try
        {
            // Act
            bool result = fileService.WriteToFile(data);

            // Assert
            Assert.True(result, "WriteToFile should return true when writing succeeds.");
            Assert.True(File.Exists(filePath), "The file should exist after WriteToFile is called.");

            // Read the content back and verify
            var json = File.ReadAllText(filePath);
            var deserializedData = JsonSerializer.Deserialize<List<string>>(json);
            Assert.NotNull(deserializedData);
            Assert.Equal(data, deserializedData);
        }
        finally
        {
            // Cleanup
            if (File.Exists(filePath))
                File.Delete(filePath);

            if (Directory.Exists(directoryPath))
                Directory.Delete(directoryPath, true);
        }
    }


}
