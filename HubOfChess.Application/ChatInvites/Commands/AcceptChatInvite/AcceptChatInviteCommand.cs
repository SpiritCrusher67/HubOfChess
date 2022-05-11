using MediatR;

namespace HubOfChess.Application.ChatInvites.Commands.AcceptChatInvite
{
    public record AcceptChatInviteCommand(Guid ChatId, Guid UserId) : IRequest;
}
