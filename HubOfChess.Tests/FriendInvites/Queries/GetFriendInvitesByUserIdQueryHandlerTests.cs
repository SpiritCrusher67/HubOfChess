using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HubOfChess.Application.FriendInvites.Queries.GetFriendInvitesByUserId;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using HubOfChess.Tests.Common;
using Xunit;

namespace HubOfChess.Tests.FriendInvites.Queries
{
    public class GetFriendInvitesByUserIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetFriendInvitesByUserIdQuery_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserB.UserId;
            var handler = new GetFriendInvitesByUserIdQueryHandler(QueryHandler, Mapper);
            var expectedInvites = Mapper.Map<IEnumerable<FriendInviteVM>>(new List<FriendInvite>
            {
                AppDbContextFactory.FriendInviteA,
                AppDbContextFactory.FriendInviteB
            });

            //Act
            var result = await handler.Handle(
                new GetFriendInvitesByUserIdQuery(userId),
                CancellationToken.None);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(expectedInvites);
        }
    }
}
