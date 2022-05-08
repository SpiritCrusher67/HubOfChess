using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IAppDbContext _dbContext;

        public DeleteMessageCommandHandler(IAppDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages
                .FirstOrDefaultAsync(m => m.Id == request.MessageId, cancellationToken);
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

            if (message == null)
                throw new NotFoundException(nameof(Message), request.MessageId);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
            if (message.Sender != user)
                throw new NoPermissionException(
                    nameof(User), user.UserId,
                    nameof(Message), message.Id);

            _dbContext.Messages.Remove(message);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
