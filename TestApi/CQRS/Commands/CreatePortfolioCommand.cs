csharp TestApi/CQRS/Commands/CreatePortfolioCommand.cs
using MediatR;
using TestApi.Models;

namespace TestApi.CQRS.Commands;

public record CreatePortfolioCommand(string Title, string ImageUrl) : IRequest<PortfolioItem>;