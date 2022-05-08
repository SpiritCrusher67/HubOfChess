using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.PostLikes.Commands
{
    public class CreatePostLikeCommandHandler : IRequestHandler<CreatePostLikeCommand>
    {
        private readonly IAppDbContext _dbContext;

        public CreatePostLikeCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreatePostLikeCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);
            var post = await _dbContext.Posts
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == request.PostId);

            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
            if (post == null)
                throw new NotFoundException(nameof(Post), request.PostId);
            if (post.Likes
                .FirstOrDefault(l => 
                    l.UserId == user.UserId && 
                    l.PostId == post.Id) != null)
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

            await _dbContext.PostLikes.AddAsync(like);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
