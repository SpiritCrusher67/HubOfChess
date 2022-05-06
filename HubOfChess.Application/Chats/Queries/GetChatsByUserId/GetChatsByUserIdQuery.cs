using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.Chats.Queries.GetChatsByUserId
{
    public record GetChatsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<Chat>>;
}
