using System.Collections.Generic;

namespace Business_Library.Interfaces;

public interface IFileService
{
    bool WriteToFile<T>(IEnumerable<T> data);
    IEnumerable<T> ReadFromFile<T>();
    bool FileExists();
}
