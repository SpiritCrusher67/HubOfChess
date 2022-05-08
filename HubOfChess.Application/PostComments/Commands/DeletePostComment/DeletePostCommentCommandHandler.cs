using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.PostComments.Commands.DeletePostComment
{
    public class DeletePostCommentCommandHandler : IRequestHandler<DeletePostCommentCommand>
    {
        private readonly IAppDbContext _dbContext;

        public DeletePostCommentCommandHandler(IAppDbContext dbContext)=>
            _dbContext = dbContext;
        
        public async Task<Unit> Handle(DeletePostCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
            var comment = await _dbContext.PostComments
                .FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
            if (comment == null)
                throw new NotFoundException(nameof(PostComment), request.CommentId);
            if (comment.User != user)
                throw new NoPermissionException(
                    nameof(User), user.UserId, 
                    nameof(PostComment), comment.Id);

            _dbContext.PostComments.Remove(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
