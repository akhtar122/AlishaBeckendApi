csharp TestApi/Controllers/PortfolioController.cs
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.CQRS.Queries;
using TestApi.CQRS.Commands;
using TestApi.Services;

namespace TestApi.Controllers;

[ApiController]
[Route("api/portfolio")]
public class PortfolioController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IFileService _files;

    public PortfolioController(IMediator mediator, IFileService files)
    {
        _mediator = mediator;
        _files = files;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var items = await _mediator.Send(new GetPortfolioQuery());
        return Ok(items);
    }

    // Admin only - accepts multipart/form-data with file and title
    [HttpPost]
    [Authorize(Roles = "admin")]
    [RequestSizeLimit(10_000_000)] // limit to ~10MB
    public async Task<IActionResult> Create([FromForm] PortfolioCreateDto dto)
    {
        if (dto.Image == null || dto.Image.Length == 0) return BadRequest("Image is required.");

        using var stream = dto.Image.OpenReadStream();
        var savedPath = _files.SaveFile(stream, dto.Image.FileName);

        var created = await _mediator.Send(new CreatePortfolioCommand(dto.Title, savedPath));
        return CreatedAtAction(null, new { created.Id, created.Title, created.ImageUrl, created.CreatedAt });
    }
}

public class PortfolioCreateDto
{
    public string Title { get; set; } = default!;
    public IFormFile? Image { get; set; }
}