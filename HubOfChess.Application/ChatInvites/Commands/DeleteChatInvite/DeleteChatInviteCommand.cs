using MediatR;

namespace HubOfChess.Application.ChatInvites.Commands.DeleteChatInvite
{
    public record DeleteChatInviteCommand(Guid ChatId, Guid UserId) : IRequest;
}
