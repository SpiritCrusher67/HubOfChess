using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HubOfChess.Application.ChatInvites.Queries.GetChatInvitesByChatId;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using HubOfChess.Tests.Common;
using Xunit;

namespace HubOfChess.Tests.ChatInvites.Queries
{
    public class GetChatInviteByChatIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetChatInvitesByChatIdQuery_Success()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatD.Id;
            var userId = AppDbContextFactory.UserA.UserId;
            var handler = new GetChatInvitesByChatIdQueryHandler(QueryHandler, Mapper);
            var expectedInvites = Mapper.Map<IEnumerable<ChatInviteVM>>(new List<ChatInvite>
            {
                AppDbContextFactory.ChatInviteA,
                AppDbContextFactory.ChatInviteB
            });

            //Act
            var result = await handler.Handle(
                new GetChatInvitesByChatIdQuery(chatId, userId),
                CancellationToken.None);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(expectedInvites);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(expectedInvites);
        }

        [Fact]
        public async Task GetChatInvitesByChatIdQueryt_SuccessEmptyResult()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatB.Id;
            var userId = AppDbContextFactory.UserC.UserId;
            var handler = new GetChatInvitesByChatIdQueryHandler(QueryHandler, Mapper);

            //Act
            var result = await handler.Handle(
                new GetChatInvitesByChatIdQuery(chatId, userId),
                CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetChatInvitesByChatIdQueryt_FailOnNotOwnerUser()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatB.Id;
            var userId = AppDbContextFactory.UserA.UserId;
            var handler = new GetChatInvitesByChatIdQueryHandler(QueryHandler, Mapper);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
               handler.Handle(
               new GetChatInvitesByChatIdQuery(chatId, userId),
               CancellationToken.None));
        }
    }
}
