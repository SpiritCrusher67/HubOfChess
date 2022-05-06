using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HubOfChess.Application.Chats.Queries.GetChatsByUserId;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Domain;
using HubOfChess.Tests.Common;
using Xunit;

namespace HubOfChess.Tests.Chats.Queries
{
    public class GetChatsByUserIDQueryHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task GetChatByIdQueryTest_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatsList = new List<Chat>
            {
                AppDbContextFactory.ChatA,
                AppDbContextFactory.ChatB,
                AppDbContextFactory.ChatC,
                AppDbContextFactory.ChatD
            };
            var handler = new GetChatsByUserIdQueryHandler(DbContext);

            //Act
            var result = await handler.Handle(
                new GetChatsByUserIdQuery(userId),
                CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(chatsList, result);
        }

        [Fact]
        public async Task GetChatByIdQueryTest_FailOnNotExistingUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var handler = new GetChatsByUserIdQueryHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                new GetChatsByUserIdQuery(userId),
                CancellationToken.None));
        }
    }
}
