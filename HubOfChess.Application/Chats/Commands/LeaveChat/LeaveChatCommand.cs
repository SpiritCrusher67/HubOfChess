using MediatR;

namespace HubOfChess.Application.Chats.Commands.LeaveChat
{
    public record LeaveChatCommand(Guid ChatId, Guid UserId) : IRequest<bool>;
}
