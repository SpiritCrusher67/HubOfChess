using MediatR;

namespace HubOfChess.Application.ChatInvites.Commands.CreateChatInvite
{
    public record CreateChatInviteCommand(Guid SenderUserId, Guid ChatId, Guid InvitedUserId, string? InviteMessage = null) : IRequest;
}
