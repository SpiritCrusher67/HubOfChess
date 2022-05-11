using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.PostLikes.Commands.CreatePostLike
{
    public class CreatePostLikeCommandHandler : IRequestHandler<CreatePostLikeCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Post> getPostHandler;

        public CreatePostLikeCommandHandler(IAppDbContext dbContext, 
            IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Post> getPostHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getPostHandler = getPostHandler;
        }

        public async Task<Unit> Handle(CreatePostLikeCommand request, CancellationToken cancellationToken)
        {
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);
            var post = await getPostHandler
                .GetEntityByIdAsync(request.PostId, cancellationToken);

            if (await dbContext.PostLikes
                .FirstOrDefaultAsync(l => l.UserId == request.UserId && 
                l.PostId == request.PostId, cancellationToken) != null)
            {
                throw new AlreadyExistException(
                    nameof(PostLike), 
                    nameof(User), user.UserId, 
                    nameof(Post), post.Id);
            }

            var like = new PostLike
            {
                UserId = user.UserId,
                PostId = post.Id,
                Date = DateTime.Now
            };

            await dbContext.PostLikes.AddAsync(like, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
