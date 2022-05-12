using FluentAssertions;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using HubOfChess.Application.ChatInvites.Commands.AcceptChatInvite;

namespace HubOfChess.Tests.ChatInvites.Commands
{
    public class AcceptChatInviteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task AcceptChatInviteCommand_Success()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatInviteC.ChatId;
            var userId = AppDbContextFactory.ChatInviteC.InvitedUserId;
            var handler = new AcceptChatInviteCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            await handler.Handle(
                new AcceptChatInviteCommand(chatId,userId),
                CancellationToken.None);

            var invite = await DbContext.ChatInvites
                .FirstOrDefaultAsync(i => i.ChatId == chatId && 
                    i.InvitedUserId == userId);
            var chat = await DbContext.Chats
                .FirstOrDefaultAsync(c => c.Id == chatId);
            var user = await DbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            //Assert
            invite.Should().BeNull();
            chat!.Users.Should().Contain(user!);
        }

        [Fact]
        public async Task AcceptChatInviteCommand_FailOnNotInvitedUser()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatInviteC.ChatId;
            var userId = AppDbContextFactory.ChatInviteC.SenderUserId;
            var handler = new AcceptChatInviteCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    new AcceptChatInviteCommand(chatId, userId),
                    CancellationToken.None));
        }
    }
}
