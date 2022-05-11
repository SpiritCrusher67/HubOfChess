using FluentAssertions;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Messages.Queries.GetMessagesByChatId;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using HubOfChess.Tests.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.Messages.Queries
{
    public class GetMessagesByChatIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetMessagesByChatIdQuery_SuccessOnFirstPage()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            (var page, var limit) = (1, 3);
            var handler = new GetMessagesByChatIdQueryHandler(QueryHandler, QueryHandler, Mapper);
            var expectedMsgList = Mapper.Map<IEnumerable<MessageVM>>(new List<Message>
            {
                AppDbContextFactory.MessageE,
                AppDbContextFactory.MessageD,
                AppDbContextFactory.MessageC
            });

            //Act
            var resultList = await handler.Handle(
                new GetMessagesByChatIdQuery(chatId, userId, page, limit),
                CancellationToken.None);

            //Assert
            Assert.NotNull(resultList);
            Assert.NotEmpty(resultList);
            resultList.Should().BeEquivalentTo(expectedMsgList);
        }

        [Fact]
        public async Task GetMessagesByChatIdQuery_SuccessOnSecondPage()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            (var page, var limit) = (2, 3);
            var handler = new GetMessagesByChatIdQueryHandler(QueryHandler, QueryHandler, Mapper);
            var expectedMsgList = Mapper.Map<IEnumerable<MessageVM>>(new List<Message>
            {
                AppDbContextFactory.MessageB,
                AppDbContextFactory.MessageA
            });

            //Act
            var resultList = await handler.Handle(
                new GetMessagesByChatIdQuery(chatId, userId, page, limit),
                CancellationToken.None);

            //Assert
            Assert.NotNull(resultList);
            Assert.NotEmpty(resultList);
            resultList.Should().BeEquivalentTo(expectedMsgList);
        }

        [Fact]
        public async Task GetMessagesByChatIdQuery_SuccessOnWrongPage()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            (var page, var limit) = (5, 3);
            var handler = new GetMessagesByChatIdQueryHandler(QueryHandler, QueryHandler, Mapper);

            //Act
            var resultList = await handler.Handle(
                new GetMessagesByChatIdQuery(chatId, userId, page, limit),
                CancellationToken.None);

            //Assert
            Assert.NotNull(resultList);
            Assert.Empty(resultList);
        }

        [Fact]
        public async Task GetMessagesByChatIdQuery_FailOnWrongUser()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            (var page, var limit) = (1, 3);
            var handler = new GetMessagesByChatIdQueryHandler(QueryHandler, QueryHandler, Mapper);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
               handler.Handle(
               new GetMessagesByChatIdQuery(chatId, userId, page, limit),
               CancellationToken.None));
        }
    }
}
