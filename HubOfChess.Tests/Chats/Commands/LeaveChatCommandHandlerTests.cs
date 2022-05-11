using System.Threading.Tasks;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Chats.Commands.LeaveChat;
using HubOfChess.Application.Common.Exceptions;
using Xunit;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Tests.Chats.Commands
{
    public class LeaveChatCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task LeaveChatCommand_NoOwnerSuccess()
        {
            //Arrange
            var userId = AppDbContextFactory.UserB.UserId;
            var chatId = AppDbContextFactory.ChatC.Id;
            var handler = new LeaveChatCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            await handler.Handle(
                new LeaveChatCommand(chatId, userId),
                CancellationToken.None);
            var user = await DbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            var chat = await DbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

            //Assert
            Assert.NotNull(chat);
            Assert.DoesNotContain(user, chat!.Users);
        }

        [Fact]
        public async Task LeaveChatCommand_OwnerSuccess()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatC.Id;
            var handler = new LeaveChatCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            await handler.Handle(
                new LeaveChatCommand(chatId, userId),
                CancellationToken.None);
            var user = await DbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            var chat = await DbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

            //Assert
            Assert.NotNull(chat);
            Assert.NotEqual(user, chat!.Owner);
            Assert.Equal(2, chat.Users.Count);
            Assert.DoesNotContain(user, chat!.Users);
        }

        [Fact]
        public async Task LeaveChatCommand_ChatRemovedSuccess()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatD.Id;
            var handler = new LeaveChatCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            var result = await handler.Handle(
                new LeaveChatCommand(chatId, userId),
                CancellationToken.None);
            var chat = await DbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

            //Assert
            Assert.True(result);
            Assert.Null(chat);
        }

        [Fact]
        public async Task LeaveChatCommand_FailOnWrongUser()
        {
            //Arrange
            var userId = AppDbContextFactory.UserB.UserId;
            var chatId = AppDbContextFactory.ChatB.Id;
            var handler = new LeaveChatCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
               handler.Handle(
               new LeaveChatCommand(chatId, userId),
               CancellationToken.None));
        }
    }
}
