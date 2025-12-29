csharp TestApi/CQRS/Queries/GetPortfolioHandler.cs
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.CQRS.Queries;

public class GetPortfolioHandler : IRequestHandler<GetPortfolioQuery, IEnumerable<PortfolioItem>>
{
    private readonly AppDbContext _db;
    public GetPortfolioHandler(AppDbContext db) => _db = db;

    public async Task<IEnumerable<PortfolioItem>> Handle(GetPortfolioQuery req, CancellationToken ct)
    {
        return await _db.Portfolio.AsNoTracking().ToListAsync(ct);
    }
}