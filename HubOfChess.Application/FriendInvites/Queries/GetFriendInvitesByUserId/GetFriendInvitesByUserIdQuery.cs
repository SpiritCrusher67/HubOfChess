using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.FriendInvites.Queries.GetFriendInvitesByUserId
{
    public record GetFriendInvitesByUserIdQuery(Guid UserId) : IRequest<IEnumerable<FriendInviteVM>>;
}
