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
            var handler = new CreateMessageCommandHandler(DbContext, QueryHandler, QueryHandler);

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
        public async Task CreateMessageCommand_FailOnWrongUser()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var text = "4AB4-8A69-381CD14A7421";
            var handler = new CreateMessageCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
               handler.Handle(
               new CreateMessageCommand(chatId, userId, text),
               CancellationToken.None));
        }
    }
}
