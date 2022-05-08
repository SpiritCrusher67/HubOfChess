using MediatR;

namespace HubOfChess.Application.Chats.Commands.DeleteChat
{
    public record DeleteChatCommand(Guid ChatId, Guid UserId) : IRequest;
}
