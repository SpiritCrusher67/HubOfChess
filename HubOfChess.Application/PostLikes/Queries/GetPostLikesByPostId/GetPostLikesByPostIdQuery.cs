using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.PostLikes.Queries.GetPostLikesByPostId
{
    public record GetPostLikesByPostIdQuery(Guid PostId) : IRequest<IEnumerable<PostLikeVM>>;
}
