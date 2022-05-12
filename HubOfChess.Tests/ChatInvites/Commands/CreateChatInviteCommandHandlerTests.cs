using FluentAssertions;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using HubOfChess.Application.ChatInvites.Commands.CreateChatInvite;

namespace HubOfChess.Tests.ChatInvites.Commands
{
    public class CreateChatInviteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateChatInviteCommand_Success()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatB.Id;
            var senderUserId = AppDbContextFactory.UserC.UserId;
            var invitedUserId = AppDbContextFactory.UserA.UserId;
            var inviteText = "B24F";
            var handler = new CreateChatInviteCommandHandler(DbContext,QueryHandler, QueryHandler);

            //Act
            await handler.Handle(
                new CreateChatInviteCommand(senderUserId, chatId,invitedUserId, inviteText),
                CancellationToken.None);
            var invite = await DbContext.ChatInvites
                .FirstOrDefaultAsync(i => i.ChatId == chatId && i.InvitedUserId == invitedUserId);

            //Assert
            invite.Should().NotBeNull();
            invite!.SenderUserId.Should().Be(senderUserId);
            invite.InviteMessage.Should().Be(inviteText);
        }

        [Fact]
        public async Task CreateChatInviteCommand_FailOnNotOwnerUser()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatB.Id;
            var senderUserId = AppDbContextFactory.UserB.UserId;
            var invitedUserId = AppDbContextFactory.UserA.UserId;
            var inviteText = "B24F";
            var handler = new CreateChatInviteCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
                handler.Handle(
                    new CreateChatInviteCommand(senderUserId, chatId, invitedUserId, inviteText),
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateChatInviteCommand_FailAlreadyExistChatInvite()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatD.Id;
            var senderUserId = AppDbContextFactory.UserA.UserId;
            var invitedUserId = AppDbContextFactory.UserB.UserId;
            var handler = new CreateChatInviteCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<AlreadyExistException>(() =>
                handler.Handle(
                    new CreateChatInviteCommand(senderUserId, chatId, invitedUserId),
                    CancellationToken.None));
        }
    }
}
