using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.FriendInvites.Commands.AcceptFriendInvite
{
    public class AcceptFriendInviteCommandHandler : IRequestHandler<AcceptFriendInviteCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;

        public AcceptFriendInviteCommandHandler(IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
        }

        public async Task<Unit> Handle(AcceptFriendInviteCommand request, CancellationToken cancellationToken)
        {
            var invite = await dbContext.FriendInvites
                .FirstOrDefaultAsync(i => i.InvitedUserId == request.UserId &&
                    i.SenderUserId == request.SederId, cancellationToken);

            if (invite == null)
                throw new NotFoundException(nameof(FriendInvite), 
                    new { request.UserId, request.SederId });

            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);
            var friend = await getUserHandler
                .GetEntityByIdAsync(request.SederId, cancellationToken);

            dbContext.FriendInvites.Remove(invite);

            var userFriend = new UserFriend
            {
                User = user,
                Friend = friend
            };
            var friendUser = new UserFriend
            {
                User = friend,
                Friend = user
            };

            await dbContext.Friends.AddRangeAsync(userFriend, friendUser);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
