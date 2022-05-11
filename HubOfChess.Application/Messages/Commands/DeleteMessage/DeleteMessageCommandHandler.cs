using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Message> getMessageHandler;

        public DeleteMessageCommandHandler(IAppDbContext dbContext, 
            IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Message> getMessageHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getMessageHandler = getMessageHandler;
        }

        public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await getMessageHandler
                .GetEntityByIdAsync(request.MessageId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            if (message.Sender != user)
                throw new NoPermissionException(
                    nameof(User), user.UserId,
                    nameof(Message), message.Id);

            dbContext.Messages.Remove(message);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
