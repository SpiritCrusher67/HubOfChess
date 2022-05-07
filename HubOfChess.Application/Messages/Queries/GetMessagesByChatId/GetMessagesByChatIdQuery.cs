using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.Messages.Queries.GetMessagesByChatId
{
    public record GetMessagesByChatIdQuery(Guid ChatId, Guid UserId, int Page, int PageLimit) : IRequest<IEnumerable<MessageVM>>;
}
