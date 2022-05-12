using FluentAssertions;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using HubOfChess.Application.FriendInvites.Commands.CreateFriendInvite;

namespace HubOfChess.Tests.FriendInvites.Commands
{
    public class CreateFriendInviteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateFriendInviteCommand_Success()
        {
            //Arrange
            var senderUserId = AppDbContextFactory.UserA.UserId;
            var invitedUserId = AppDbContextFactory.UserC.UserId;
            var inviteText = "A45G";
            var handler = new CreateFriendInviteCommandHandler(DbContext, QueryHandler);

            //Act
            await handler.Handle(
                new CreateFriendInviteCommand(senderUserId, invitedUserId, inviteText),
                CancellationToken.None);
            var invite = await DbContext.FriendInvites
                .FirstOrDefaultAsync(i => i.SenderUserId == senderUserId 
                    && i.InvitedUserId == invitedUserId);

            //Assert
            invite.Should().NotBeNull();
            invite!.InviteMessage.Should().Be(inviteText);
        }

        [Fact]
        public async Task CreateFriendInviteCommand_FailOnExistingInvite()
        {
            //Arrange
            var senderUserId = AppDbContextFactory.UserA.UserId;
            var invitedUserId = AppDbContextFactory.UserB.UserId;
            var handler = new CreateFriendInviteCommandHandler(DbContext, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<AlreadyExistException>(() =>
                handler.Handle(
                    new CreateFriendInviteCommand(senderUserId, invitedUserId),
                    CancellationToken.None));
        }
    }
}
