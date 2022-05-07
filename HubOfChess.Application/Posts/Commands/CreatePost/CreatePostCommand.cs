using MediatR;

namespace HubOfChess.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand(Guid UserId, string Title, string Text) : IRequest<Guid>;
}
