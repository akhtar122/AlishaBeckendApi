csharp TestApi/Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models;

public class User
{
    public int Id { get; set; }
    [Required] public string Username { get; set; } = default!;
    [Required] public string PasswordHash { get; set; } = default!;
    [Required] public string Role { get; set; } = "manager";
}