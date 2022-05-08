using MediatR;

namespace HubOfChess.Application.Chats.Commands.CreateChat
{
    public record CreateChatCommand(Guid ChatOwner, string? ChatName = null) : IRequest<Guid>;
}
