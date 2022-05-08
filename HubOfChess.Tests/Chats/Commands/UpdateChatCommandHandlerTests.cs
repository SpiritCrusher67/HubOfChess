using System.Threading.Tasks;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Chats.Commands.UpdateChat;
using Xunit;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Application.Common.Exceptions;
using System;

namespace HubOfChess.Tests.Chats.Commands
{
    public class UpdateChatCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateChatCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var ownerUserId = AppDbContextFactory.UserB.UserId;
            var chatName = "D7E1F0BD";
            var handler = new UpdateChatCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new UpdateChatCommand(chatId, userId, ownerUserId, chatName), 
                CancellationToken.None);
            var chat = await DbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

            //Assert
            Assert.NotNull(chat);
            Assert.Equal(ownerUserId, chat!.Owner!.UserId);
            Assert.Equal(chatName, chat.Name);
        }

        [Fact]
        public async Task UpdateChatCommand_FailOnWrongNewOwnerUser()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var chatId = AppDbContextFactory.ChatB.Id;
            var ownerUserId = AppDbContextFactory.UserB.UserId;
            var handler = new UpdateChatCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new UpdateChatCommand(chatId, userId, ownerUserId),
                CancellationToken.None);
            var chat = await DbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

            //Assert
            Assert.NotNull(chat);
            Assert.Equal(userId, chat!.Owner!.UserId);
        }

        [Fact]
        public async Task UpdateChatCommand_FailOnWrongUser()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var ownerUserId = AppDbContextFactory.UserB.UserId;
            var chatName = "D7E1F0BD";
            var handler = new UpdateChatCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>( () => 
                handler.Handle(
                new UpdateChatCommand(chatId, userId, ownerUserId, chatName),
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateChatCommand_FailOnNotExistingUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var chatId = AppDbContextFactory.ChatA.Id;
            var ownerUserId = AppDbContextFactory.UserB.UserId;
            var chatName = "D7E1F0BD";
            var handler = new UpdateChatCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.Handle(
               new UpdateChatCommand(chatId, userId, ownerUserId, chatName),
               CancellationToken.None));
        }

        [Fact]
        public async Task UpdateChatCommand_FailOnNoExistingChat()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var chatId = Guid.NewGuid();
            var ownerUserId = AppDbContextFactory.UserB.UserId;
            var chatName = "D7E1F0BD";
            var handler = new UpdateChatCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.Handle(
               new UpdateChatCommand(chatId, userId, ownerUserId, chatName),
               CancellationToken.None));
        }
    }
}
