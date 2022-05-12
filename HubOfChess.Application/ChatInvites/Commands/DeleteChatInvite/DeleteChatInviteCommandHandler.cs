using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.ChatInvites.Commands.DeleteChatInvite
{
    public class DeleteChatInviteCommandHandler : IRequestHandler<DeleteChatInviteCommand>
    {
        private readonly IAppDbContext dbContext;

        public DeleteChatInviteCommandHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteChatInviteCommand request, CancellationToken cancellationToken)
        {
            var invite = await dbContext.ChatInvites
                .FirstOrDefaultAsync(i => i.ChatId == request.ChatId && 
                    (i.SenderUserId == request.UserId  
                    || i.InvitedUserId == request.UserId));

            if (invite == null)
                throw new NotFoundException(nameof(ChatInvite), 
                    new { request.ChatId, SenderOrInvitedUserId = request.UserId });

            dbContext.ChatInvites.Remove(invite);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
