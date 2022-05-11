using MediatR;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Domain;

namespace HubOfChess.Application.Chats.Commands.UpdateChat
{
    public class UpdateChatCommandHandler : IRequestHandler<UpdateChatCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;

        public UpdateChatCommandHandler(IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Chat> getChatHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getChatHandler = getChatHandler;
        }

        public async Task<Unit> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);
            var ownerUser = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.ChatOwnerId, cancellationToken);

            if (chat.Owner != user)
                throw new NoPermissionException(
                    nameof(User), user.UserId, 
                    nameof(Chat), chat.Id);

            chat.Name = request.ChatName;
            if (ownerUser != null && chat.Users.Contains(ownerUser))
                chat.Owner = ownerUser;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
