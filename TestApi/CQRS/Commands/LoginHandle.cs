
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TestApi.CQRS.Commands;

public class LoginHandler : IRequestHandler<LoginCommand, int>
{
    private readonly AppDbContext _db;
    public LoginHandler(AppDbContext db) => _db = db;

    public async Task<int> Handle(LoginCommand req, CancellationToken ct)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == req.Username, ct);
        if (user == null) throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");
        return user.Id;
    }
}
