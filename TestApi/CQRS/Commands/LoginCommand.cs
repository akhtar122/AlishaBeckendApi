using MediatR;

namespace TestApi.CQRS.Commands;

public record LoginCommand(string Username, string Password) : IRequest<int /*userId*/>;
