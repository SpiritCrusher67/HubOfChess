using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.ChatInvites.Queries.GetChatInvitesByUserId
{
    public record GetChatInvitesByUserIdQuery(Guid UserId) : IRequest<IEnumerable<ChatInviteVM>>;
}
