using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApi.Data;
using TestApi.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace TestApi.CQRS.Commands;

public class SignupHandler : IRequestHandler<SignupCommand, User>
{
    private readonly AppDbContext _db;
    public SignupHandler(AppDbContext db) => _db = db;

    public async Task<User> Handle(SignupCommand req, CancellationToken ct)
    {
        if (await _db.Users.AnyAsync(u => u.Username == req.Username, ct))
            throw new InvalidOperationException("Username already exists.");

        var hash = BCrypt.Net.BCrypt.HashPassword(req.Password);
        var user = new User { Username = req.Username, PasswordHash = hash, Role = req.Role ?? "manager" };
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
        return user;
    }
}
