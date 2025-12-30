using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using MediatR;
using TestApi.CQRS.Queries;
using TestApi.Models;

namespace TestApi.GraphQL;

public class Query
{
    public async Task<IEnumerable<PortfolioItem>> GetPortfolio([Service] IMediator mediator)
    {
        return await mediator.Send(new GetPortfolioQuery());
    }
}