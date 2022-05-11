using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HubOfChess.Application.Chats.Queries.GetChatById;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Tests.Common;
using Xunit;

namespace HubOfChess.Tests.Chats.Queries
{
    public class GetChatByIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetChatByIdQueryTest_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var handler = new GetChatByIdQueryHandler(QueryHandler, QueryHandler, Mapper);

            //Act
            var chat = await handler.Handle(
                new GetChatByIdQuery(chatId,userId),
                CancellationToken.None);

            //Assert
            chat.Should().NotBeNull();
            chat.Id.Should().Be(chatId);
            chat.Name.Should().Be(AppDbContextFactory.ChatA.Name);
        }

        [Fact]
        public async Task GetChatByIdQueryTest_FailOnWrongUserId()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var handler = new GetChatByIdQueryHandler(QueryHandler, QueryHandler, Mapper);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
               handler.Handle(
               new GetChatByIdQuery(chatId, userId),
               CancellationToken.None));
        }
    }
}
