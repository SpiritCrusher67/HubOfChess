using FluentAssertions;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using HubOfChess.Application.FriendInvites.Commands.AcceptFriendInvite;

namespace HubOfChess.Tests.FriendInvites.Commands
{
    public class AcceptFriendInviteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task AcceptFriendInviteCommand_Success()
        {
            //Arrange
            var senderUserId = AppDbContextFactory.UserA.UserId;
            var userId = AppDbContextFactory.UserB.UserId;
            var handler = new AcceptFriendInviteCommandHandler(DbContext, QueryHandler);

            //Act
            await handler.Handle(
                new AcceptFriendInviteCommand(userId, senderUserId),
                CancellationToken.None);

            var user = await DbContext.Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            var userFriend = await DbContext.Friends
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == senderUserId);
            var friendUser = await DbContext.Friends
                .FirstOrDefaultAsync(f => f.UserId == senderUserId && f.FriendId == userId);
            var invite = await DbContext.FriendInvites
                .FirstOrDefaultAsync(i => i.SenderUserId == senderUserId && i.InvitedUserId == userId);

            //Assert
            user.Should().NotBeNull();
            userFriend.Should().NotBeNull();
            friendUser.Should().NotBeNull();
            invite.Should().BeNull();
            user!.FriendInvites.Should().NotBeNullOrEmpty();
            user.Friends.Should().Contain(userFriend!);
        }

        [Fact]
        public async Task AcceptFriendInviteCommand_FailOnNotExistingInvite()
        {
            //Arrange
            var senderUserId = AppDbContextFactory.UserA.UserId;
            var userId = AppDbContextFactory.UserA.UserId;
            var handler = new AcceptFriendInviteCommandHandler(DbContext, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    new AcceptFriendInviteCommand(userId, senderUserId),
                    CancellationToken.None));
        }
    }
}
