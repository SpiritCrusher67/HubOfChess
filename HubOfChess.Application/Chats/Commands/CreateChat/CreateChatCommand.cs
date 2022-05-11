using MediatR;

namespace HubOfChess.Application.Chats.Commands.CreateChat
{
    public record CreateChatCommand(Guid ChatOwnerUserId, string? ChatName = null) : IRequest<Guid>;
}
