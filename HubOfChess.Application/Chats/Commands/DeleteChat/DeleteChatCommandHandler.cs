using MediatR;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using HubOfChess.Application.Common.Exceptions;

namespace HubOfChess.Application.Chats.Commands.DeleteChat
{
    public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;

        public DeleteChatCommandHandler(IAppDbContext dbContext, IGetEntityQueryHandler<Chat> getChatHandler)
        {
            this.dbContext = dbContext;
            this.getChatHandler = getChatHandler;
        }

        public async Task<Unit> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);

            if (chat.Owner?.UserId != request.UserId)
                throw new NoPermissionException(
                    nameof(User), request.UserId, 
                    nameof(Chat), request.ChatId);

            dbContext.Chats.Remove(chat);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
