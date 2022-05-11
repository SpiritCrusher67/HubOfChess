using MediatR;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Domain;

namespace HubOfChess.Application.Chats.Queries.GetChatById
{
    public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, Chat>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;

        public GetChatByIdQueryHandler(IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Chat> getChatHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getChatHandler = getChatHandler;
        }

        public async Task<Chat> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            if (!chat.Users.Contains(user))
                throw new NoPermissionException(
                    nameof(User),user.UserId, 
                    nameof(Chat), chat.Id);

            return chat;
        }
    }
}
