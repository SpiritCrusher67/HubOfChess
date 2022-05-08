using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid UserId) : IRequest<UserVM>;
}
