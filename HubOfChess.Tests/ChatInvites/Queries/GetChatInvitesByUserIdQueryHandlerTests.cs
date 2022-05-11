using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HubOfChess.Application.ChatInvites.Queries.GetChatInvitesByUserId;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using HubOfChess.Tests.Common;
using Xunit;

namespace HubOfChess.Tests.ChatInvites.Queries
{
    public class GetChatInvitesByUserIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetChatInvitesByUserIdQuery_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var handler = new GetChatInvitesByUserIdQueryHandler(DbContext, Mapper);
            var expectedInvites = Mapper.Map<IEnumerable<ChatInviteVM>>(new List<ChatInvite>
            {
                AppDbContextFactory.ChatInviteB,
                AppDbContextFactory.ChatInviteC
            });

            //Act
            var result = await handler.Handle(
                new GetChatInvitesByUserIdQuery(userId),
                CancellationToken.None);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(expectedInvites);
        }

        [Fact]
        public async Task GetChatInvitesByUserIdQueryt_SuccessEmptyResult()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var handler = new GetChatInvitesByUserIdQueryHandler(DbContext, Mapper);

            //Act
            var result = await handler.Handle(
                new GetChatInvitesByUserIdQuery(userId),
                CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetChatInvitesByUserIdQueryt_FailOnWrongUserId()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var handler = new GetChatInvitesByUserIdQueryHandler(DbContext, Mapper);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.Handle(
               new GetChatInvitesByUserIdQuery(userId),
               CancellationToken.None));
        }
    }
}
