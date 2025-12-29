csharp TestApi/Services/FileService.cs
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TestApi.Services;

public class FileService : IFileService
{
    private readonly string _basePath;
    public string BasePath => _basePath;

    public FileService(IConfiguration conf)
    {
        // Default folder is D:\JwtApp per requirement
        _basePath = conf["FileStorage:BasePath"] ?? @"D:\JwtApp";
    }

    public void EnsureBasePathExists()
    {
        if (!Directory.Exists(_basePath))
            Directory.CreateDirectory(_basePath);
    }

    public string SaveFile(Stream fileStream, string originalFileName)
    {
        EnsureBasePathExists();
        var ext = Path.GetExtension(originalFileName);
        var fileName = $"{Guid.NewGuid():N}{ext}";
        var full = Path.Combine(_basePath, fileName);
        using var fs = File.Create(full);
        fileStream.CopyTo(fs);
        // Return full path (store in DB)
        return full;
    }
}