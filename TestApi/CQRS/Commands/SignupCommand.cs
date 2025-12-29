using MediatR;
using TestApi.Models;

namespace TestApi.CQRS.Commands;

public record SignupCommand(string Username, string Password, string Role = "manager") : IRequest<User>;