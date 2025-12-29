csharp TestApi/CQRS/Queries/GetPortfolioQuery.cs
using MediatR;
using TestApi.Models;
using System.Collections.Generic;

namespace TestApi.CQRS.Queries;

public record GetPortfolioQuery() : IRequest<IEnumerable<PortfolioItem>>;