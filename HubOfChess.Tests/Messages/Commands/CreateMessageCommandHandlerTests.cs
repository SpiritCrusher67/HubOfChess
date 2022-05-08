using System;
using System.Threading;
using System.Threading.Tasks;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Messages.Commands.CreateMessage;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HubOfChess.Tests.Messages.Commands
{
    public class CreateMessageCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateMessageCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var text = "4AB4-8A69-381CD14A7421";
            var handler = new CreateMessageCommandHandler(DbContext);

            //Act
            var msgId = await handler.Handle(
                new CreateMessageCommand(chatId, userId, text),
                CancellationToken.None);
            var msg = await DbContext.Messages.FirstOrDefaultAsync(m => m.Id == msgId);

            //Assert
            Assert.NotNull(msg);
            Assert.Equal(userId, msg!.Sender.UserId);
            Assert.Equal(chatId, msg!.Chat.Id);
            Assert.Equal(text, msg!.Text);
        }

        [Fact]
        public async Task LeaveChatCommand_FailOnNotExistingChat()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = Guid.NewGuid();
            var text = "4AB4-8A69-381CD14A7421";
            var handler = new CreateMessageCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.Handle(
               new CreateMessageCommand(chatId, userId, text),
               CancellationToken.None));
        }

        [Fact]
        public async Task LeaveChatCommand_FailOnNotExistingUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var chatId = AppDbContextFactory.ChatA.Id;
            var text = "4AB4-8A69-381CD14A7421";
            var handler = new CreateMessageCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.Handle(
               new CreateMessageCommand(chatId, userId, text),
               CancellationToken.None));
        }

        [Fact]
        public async Task LeaveChatCommand_FailOnWrongUser()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var text = "4AB4-8A69-381CD14A7421";
            var handler = new CreateMessageCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
               handler.Handle(
               new CreateMessageCommand(chatId, userId, text),
               CancellationToken.None));
        }
    }
}
