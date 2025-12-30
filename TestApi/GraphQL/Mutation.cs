using HotChocolate.AspNetCore.Authorization;
using System.Threading.Tasks;
using MediatR;
using TestApi.CQRS.Commands;
using TestApi.Models;
using Microsoft.AspNetCore.Authorization;
using HotChocolate;

namespace TestApi.GraphQL;

public class Mutation
{
    // GraphQL mutation for creating portfolio requires admin role
    [Authorize(Roles = "admin")]
    public async Task<PortfolioItem> CreatePortfolio([Service] IMediator mediator, string title, string imageUrl)
    {
        return await mediator.Send(new CreatePortfolioCommand(title, imageUrl));
    }
}