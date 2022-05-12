using MediatR;

namespace HubOfChess.Application.FriendInvites.Commands.AcceptFriendInvite
{
    public record AcceptFriendInviteCommand(Guid UserId, Guid SederId) : IRequest;
}
