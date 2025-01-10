using System.IO;
using Business_Library.Repositories;
using Business_Library.Services;

namespace Business_Library.Helpers
{
    public static class TestHelpers
    {
        // Constants for test data directory and file name
        public const string DirectoryPath = "TestData"; // Made public
        public const string FileName = "testUsers.json"; // Made public

        // Full file path for the test data
        public static string FilePath => Path.Combine(DirectoryPath, FileName);

        // Helper method to create a UserRepository instance with a FileService
        public static UserRepository CreateUserRepository()
        {
            var fileService = new FileService(DirectoryPath, FileName);
            return new UserRepository(fileService, FilePath);
        }

        // Cleanup method to ensure test artifacts are removed
        public static void Cleanup()
        {
            try
            {
                if (File.Exists(FilePath))
                    File.Delete(FilePath);

                if (Directory.Exists(DirectoryPath))
                    Directory.Delete(DirectoryPath, true); // Force delete directory
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }
}
