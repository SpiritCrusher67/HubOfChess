using HubOfChess.Application.Chats.Commands.CreateChat;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.Chats.Commands
{
    public class CreateChatCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateChatCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var name = "84466A55";
            var handler = new CreateChatCommandHandler(DbContext, QueryHandler);

            //Act
            var chatId = await handler.Handle(new CreateChatCommand(userId, name), CancellationToken.None);

            //Assert
            Assert.NotNull(await DbContext.Chats.SingleOrDefaultAsync(c => c.Id == chatId));
        }
    }
}
