using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.Chats.Commands.CreateChat
{
    public record CreateChatCommand(User ChatOwner, string? ChatName = null) 
        : IRequest<Guid>;
}
