using MediatR;

namespace HubOfChess.Application.Chats.Commands.UpdateChat
{
    public record UpdateChatCommand(Guid ChatId, Guid UserId, Guid? ChatOwnerId = null, string? ChatName = null) : IRequest;

}
