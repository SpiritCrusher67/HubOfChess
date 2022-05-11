using FluentAssertions;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using HubOfChess.Application.ChatInvites.Commands.DeleteChatInvite;
using System;

namespace HubOfChess.Tests.ChatInvites.Commands
{
    public class DeleteChatInviteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteChatInviteCommand_SuccessBySenderUser()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatInviteC.ChatId;
            var userId = AppDbContextFactory.ChatInviteC.SenderUserId;
            var handler = new DeleteChatInviteCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new DeleteChatInviteCommand(chatId, userId),
                CancellationToken.None);
            var invite = await DbContext.ChatInvites
                .FirstOrDefaultAsync(i => i.ChatId == chatId && 
                    i.InvitedUserId == AppDbContextFactory.ChatInviteC.InvitedUserId);

            //Assert
            invite.Should().BeNull();
        }

        [Fact]
        public async Task DeleteChatInviteCommand_SuccessByInvitedUser()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatInviteC.ChatId;
            var userId = AppDbContextFactory.ChatInviteC.InvitedUserId;
            var handler = new DeleteChatInviteCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new DeleteChatInviteCommand(chatId, userId),
                CancellationToken.None);
            var invite = await DbContext.ChatInvites
                .FirstOrDefaultAsync(i => i.ChatId == chatId && i.InvitedUserId == userId);

            //Assert
            invite.Should().BeNull();
        }

        [Fact]
        public async Task CreateChatInviteCommand_FailOnNotWrongUserId()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatInviteC.ChatId;
            var userId = Guid.NewGuid();
            var handler = new DeleteChatInviteCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    new DeleteChatInviteCommand(chatId,userId),
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateChatInviteCommand_FailOnNotWrongChatId()
        {
            //Arrange
            var chatId = Guid.NewGuid();
            var userId = AppDbContextFactory.ChatInviteC.InvitedUserId;
            var handler = new DeleteChatInviteCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    new DeleteChatInviteCommand(chatId, userId),
                    CancellationToken.None));
        }
    }
}
