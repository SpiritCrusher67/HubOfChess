using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.Chats.Queries.GetChatById
{
    public record GetChatByIdQuery(Guid ChatId, Guid UserId) : IRequest<ChatVM>;
}
