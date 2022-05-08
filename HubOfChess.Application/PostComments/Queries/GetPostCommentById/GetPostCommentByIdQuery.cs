using HubOfChess.Application.ViewModels;
using MediatR;

namespace HubOfChess.Application.PostComments.Queries.GetPostCommentById
{
    public record GetPostCommentByIdQuery(Guid CommentId) : IRequest<PostCommentVM>;
}
