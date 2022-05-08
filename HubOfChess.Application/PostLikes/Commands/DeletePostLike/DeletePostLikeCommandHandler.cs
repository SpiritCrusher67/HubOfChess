using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.PostLikes.Commands.DeletePostLike
{
    public class DeletePostLikeCommandHandler : IRequestHandler<DeletePostLikeCommand>
    {
        private readonly IAppDbContext _dbContext;

        public DeletePostLikeCommandHandler(IAppDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePostLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _dbContext.PostLikes
                .FirstOrDefaultAsync(l => l.UserId == request.UserId && 
                    l.PostId == request.PostId);
            if (like == null)
                throw new NotFoundException(nameof(PostLike), new { request.PostId, request.UserId });

            _dbContext.PostLikes.Remove(like);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
