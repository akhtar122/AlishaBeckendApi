using System.ComponentModel.DataAnnotations;

namespace TestApi.Models;

public class PortfolioItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

}