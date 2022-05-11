using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.PostComments.Commands.CreatePostComment
{
    public class CreatePostCommentCommandHandler : IRequestHandler<CreatePostCommentCommand, Guid>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Post> getPostHandler;

        public CreatePostCommentCommandHandler(IAppDbContext dbContext, 
            IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Post> getPostHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getPostHandler = getPostHandler;
        }

        public async Task<Guid> Handle(CreatePostCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await getPostHandler
                .GetEntityByIdAsync(request.PostId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            var comment = new PostComment
            {
                Id = Guid.NewGuid(),
                User = user,
                Post = post,
                Text = request.Text,
                Date = DateTime.Now
            };

            await dbContext.PostComments.AddAsync(comment, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}
