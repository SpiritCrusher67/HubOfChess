using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.ChatInvites.Queries.GetChatInvitesByChatId
{
    public record GetChatInvitesByChatIdQuery(Guid ChatId, Guid UserId) : IRequest<IEnumerable<ChatInviteVM>>;
}
