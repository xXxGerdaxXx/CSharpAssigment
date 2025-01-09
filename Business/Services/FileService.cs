using Business_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Business_Library.Services;

/// <summary>
/// Provides file handling services for reading, writing, checking the existence of files.
/// Supports generic data serialization and deserialization using JSON format.
/// </summary>

public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions _jsonOptions = new ()
    {
        WriteIndented = true
    };

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
            var json = JsonSerializer.Serialize(data, _jsonOptions);
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
                return [];

            var json = File.ReadAllText(_filePath);

            // Check if the file stores a single object or a collection of objects
            if (json.Trim().StartsWith("{"))
            {
                var singleObject = JsonSerializer.Deserialize<T>(json, _jsonOptions);
                return singleObject != null ? new List<T> { singleObject } : [];
            }

            return JsonSerializer.Deserialize<IEnumerable<T>>(json, _jsonOptions) ?? [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading from file: {ex.Message}");
            return [];
        }
    }

    public bool FileExists()
    {
        return File.Exists(_filePath);
    }
}
