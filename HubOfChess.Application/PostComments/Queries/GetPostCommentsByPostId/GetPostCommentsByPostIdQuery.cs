using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.PostComments.Queries.GetPostCommentsByPostId
{
    public record GetPostCommentsByPostIdQuery(Guid PostId) : IRequest<IEnumerable<PostCommentVM>>;

}
