using MediatR;

namespace HubOfChess.Application.FriendInvites.Commands.CreateFriendInvite
{
    public record CreateFriendInviteCommand(Guid SenderUserId, Guid InvitedUserId, string? InviteMessage = null) : IRequest;
}
