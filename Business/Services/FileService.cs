using Business_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Business_Library.Services;

public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;

    public FileService(string directoryPath, string fileName)
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(directoryPath, fileName);

        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
    }

    public bool WriteToFile<T>(IEnumerable<T> data)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error writing to file: {ex.Message}");
            return false;
        }
    }

    public IEnumerable<T> ReadFromFile<T>()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            var json = File.ReadAllText(_filePath);

            if (json.Trim().StartsWith("{"))
            {
                var singleObject = JsonSerializer.Deserialize<T>(json);
                return singleObject != null ? new List<T> { singleObject } : new List<T>();
            }

            return JsonSerializer.Deserialize<IEnumerable<T>>(json) ?? new List<T>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading from file: {ex.Message}");
            return new List<T>();
        }
    }

    public bool FileExists()
    {
        return File.Exists(_filePath);
    }
}
