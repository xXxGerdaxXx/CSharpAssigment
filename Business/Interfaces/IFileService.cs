using System.Collections.Generic;

namespace Business_Library.Interfaces;


/// <summary>
/// Interface for reading, writing and checking if file exists
/// </summary>
public interface IFileService
{
    bool WriteToFile<T>(IEnumerable<T> data);
    IEnumerable<T> ReadFromFile<T>();
    bool FileExists();
}
