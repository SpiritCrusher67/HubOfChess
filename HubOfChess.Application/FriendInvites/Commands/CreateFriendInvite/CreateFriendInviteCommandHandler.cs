using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.FriendInvites.Commands.CreateFriendInvite
{
    public class CreateFriendInviteCommandHandler : IRequestHandler<CreateFriendInviteCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;

        public CreateFriendInviteCommandHandler(IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
        }

        public async Task<Unit> Handle(CreateFriendInviteCommand request, CancellationToken cancellationToken)
        {
            var senderUser = await getUserHandler
                .GetEntityByIdAsync(request.SenderUserId, cancellationToken);
            var invitedUser = await getUserHandler
                .GetEntityByIdAsync(request.InvitedUserId, cancellationToken);

            bool isFriend = dbContext.Friends
                .FirstOrDefault(f => f.UserId == senderUser.UserId &&
                    f.FriendId == invitedUser.UserId) != null;
            bool isAlreadyInvited = senderUser.SendedFriendInvites
                .FirstOrDefault(i => i.InvitedUserId == invitedUser.UserId) != null;

            if (isFriend || isAlreadyInvited)
                throw new AlreadyExistException(
                    isFriend ? nameof(UserFriend) : nameof(FriendInvite), 
                    nameof(User), senderUser.UserId, 
                    nameof(User), invitedUser.UserId);

            var invite = new FriendInvite
            {
                InvitedUser = invitedUser,
                SenderUser = senderUser,
                InviteMessage = request.InviteMessage,
                Date = DateTime.Now
            };

            await dbContext.FriendInvites.AddAsync(invite, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
