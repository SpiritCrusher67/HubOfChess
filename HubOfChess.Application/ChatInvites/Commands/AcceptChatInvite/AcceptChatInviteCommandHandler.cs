using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.ChatInvites.Commands.AcceptChatInvite
{
    public class AcceptChatInviteCommandHandler : IRequestHandler<AcceptChatInviteCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;

        public AcceptChatInviteCommandHandler(IAppDbContext dbContext, 
            IGetEntityQueryHandler<User> getUserHandler, 
            IGetEntityQueryHandler<Chat> getChatHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getChatHandler = getChatHandler;
        }

        public async Task<Unit> Handle(AcceptChatInviteCommand request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);
            var invite = await dbContext.ChatInvites
                .FirstOrDefaultAsync(i => i.ChatId == request.ChatId 
                    && i.InvitedUserId == request.UserId, 
                    cancellationToken);
            if (invite == null)
                throw new NotFoundException(nameof(ChatInvite), 
                    new { request.ChatId, InvitedUserId = request.UserId });

            dbContext.ChatInvites.Remove(invite);
            chat.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
