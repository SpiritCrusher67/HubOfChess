using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.Chats.Queries.GetChatsByUserId
{
    public record GetChatsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<ChatVM>>;
}
