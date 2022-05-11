using MediatR;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Domain;

namespace HubOfChess.Application.Chats.Commands.LeaveChat
{
    public class LeaveChatCommandHandler : IRequestHandler<LeaveChatCommand, bool>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;

        public LeaveChatCommandHandler(
            IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Chat> getChatHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getChatHandler = getChatHandler;
        }

        public async Task<bool> Handle(LeaveChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            if (!chat.Users.Contains(user))
                throw new NoPermissionException(
                    nameof(User), user.UserId, nameof(Chat), chat.Id);

            if (!chat.Users.Remove(user))
                return false;

            if (chat.Owner == user)
                chat.Owner = chat.Users.FirstOrDefault(u => u.UserId != user.UserId);

            if (chat.Users.Count == 0)
                dbContext.Chats.Remove(chat);

            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
