using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.CQRS.Commands;

public class CreatePortfolioHandler : IRequestHandler<CreatePortfolioCommand, PortfolioItem>
{
    private readonly AppDbContext _db;
    public CreatePortfolioHandler(AppDbContext db) => _db = db;

    public async Task<PortfolioItem> Handle(CreatePortfolioCommand req, CancellationToken ct)
    {
        var item = new PortfolioItem { Title = req.Title, ImageUrl = req.ImageUrl, CreatedAt = DateTime.UtcNow };
        _db.Portfolio.Add(item);
        await _db.SaveChangesAsync(ct);
        return item;
    }
}