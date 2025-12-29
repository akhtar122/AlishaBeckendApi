csharp TestApi/Models/PortfolioItem.cs
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models;

public class PortfolioItem
{
    public int Id { get; set; }
    [Required] public string Title { get; set; } = default!;
    // store full file path or URL (per your requirement)
    [Required] public string ImageUrl { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}