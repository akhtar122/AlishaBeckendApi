namespace TestApi.Services;

public interface IFileService
{
    string SaveFile(Stream fileStream, string originalFileName);
    void EnsureBasePathExists();
    string BasePath { get; }
}