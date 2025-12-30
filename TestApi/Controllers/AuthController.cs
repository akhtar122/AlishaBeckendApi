using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApi.CQRS.Commands;
using TestApi.Services;
using TestApi.Data;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJwtService _jwt;
    private readonly AppDbContext _db;

    public AuthController(IMediator mediator, IJwtService jwt, AppDbContext db)
    {
        _mediator = mediator;
        _jwt = jwt;
        _db = db;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupDto dto)
    {
        // Only admin can assign role
        if (!string.IsNullOrWhiteSpace(dto.Role))
        {
            if (!User.Identity?.IsAuthenticated ?? true || !User.IsInRole("admin"))
                return Forbid();
        }

        var user = await _mediator.Send(new SignupCommand(dto.Username, dto.Password, dto.Role ?? "manager"));
        return CreatedAtAction(null, new { user.Id, user.Username, user.Role });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized();

        var token = _jwt.GenerateToken(user.Id, user.Username, user.Role);

        // set as cookie
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(8)
        });

        return Ok(new { token });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        if (Request.Cookies.ContainsKey("jwt"))
        {
            Response.Cookies.Append("jwt", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }
        return Ok(new { message = "Logged out" });
    }
}

public record SignupDto(string Username, string Password, string? Role);
public record LoginDto(string Username, string Password);