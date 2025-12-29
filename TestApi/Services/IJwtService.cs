csharp TestApi/Services/IJwtService.cs
namespace TestApi.Services;

public interface IJwtService
{
    string GenerateToken(int userId, string username, string role);
}