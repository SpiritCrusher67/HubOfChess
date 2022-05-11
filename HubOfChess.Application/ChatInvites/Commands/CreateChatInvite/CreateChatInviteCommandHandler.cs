using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.ChatInvites.Commands.CreateChatInvite
{
    public class CreateChatInviteCommandHandler : IRequestHandler<CreateChatInviteCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;

        public CreateChatInviteCommandHandler(IAppDbContext dbContext, 
            IGetEntityQueryHandler<User> getUserHandler,
            IGetEntityQueryHandler<Chat> getChatHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getChatHandler = getChatHandler;
        }

        public async Task<Unit> Handle(CreateChatInviteCommand request, CancellationToken cancellationToken)
        {
            var senderUser = await getUserHandler
                .GetEntityByIdAsync(request.SenderUserId, cancellationToken);
            var invitedUser = await getUserHandler
                .GetEntityByIdAsync(request.InvitedUserId, cancellationToken);
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);

            if (senderUser != chat.Owner)
                throw new NoPermissionException(
                    nameof(User), senderUser.UserId,
                    nameof(Chat), chat.Id);

            var invite = new ChatInvite
            {
                Chat = chat,
                SenderUser = senderUser,
                InvitedUser = invitedUser,
                InviteMessage = request.InviteMessage
            };

            await dbContext.ChatInvites.AddAsync(invite, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
