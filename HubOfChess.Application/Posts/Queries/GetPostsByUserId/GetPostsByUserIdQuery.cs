using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.Posts.Queries.GetPostsByUserId
{
    public record GetPostsByUserIdQuery(Guid UserId, int Page, int PageLimit) : IRequest<IEnumerable<PostVM>>;
}
