using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HubOfChess.Application.Chats.Queries.GetChatsByUserId;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using HubOfChess.Tests.Common;
using Xunit;

namespace HubOfChess.Tests.Chats.Queries
{
    public class GetChatsByUserIDQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetChatByIdQueryTest_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatsVMList = Mapper.Map<IEnumerable<ChatVM>>( new List<Chat>
            {
                AppDbContextFactory.ChatA,
                AppDbContextFactory.ChatB,
                AppDbContextFactory.ChatC,
                AppDbContextFactory.ChatD
            });
            var handler = new GetChatsByUserIdQueryHandler(QueryHandler,Mapper);

            //Act
            var result = await handler.Handle(
                new GetChatsByUserIdQuery(userId),
                CancellationToken.None);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(chatsVMList);
        }
    }
}
