using System.Threading;
using System.Threading.Tasks;
using HubOfChess.Application.Chats.Queries.GetChatById;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Tests.Common;
using Xunit;

namespace HubOfChess.Tests.Chats.Queries
{
    public class GetChatByIdQueryHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task GetChatByIdQueryTest_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var handler = new GetChatByIdQueryHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            var chat = await handler.Handle(
                new GetChatByIdQuery(chatId,userId),
                CancellationToken.None);

            //Assert
            Assert.NotNull(chat);
            Assert.Equal(chatId, chat.Id);
            Assert.Equal(AppDbContextFactory.ChatA, chat);
        }

        [Fact]
        public async Task GetChatByIdQueryTest_FailOnWrongUserId()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var handler = new GetChatByIdQueryHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
               handler.Handle(
               new GetChatByIdQuery(chatId, userId),
               CancellationToken.None));
        }
    }
}
