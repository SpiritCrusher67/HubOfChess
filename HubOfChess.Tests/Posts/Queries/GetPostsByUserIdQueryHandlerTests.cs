using FluentAssertions;
using HubOfChess.Application.Posts.Queries.GetPostsByUserId;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using HubOfChess.Tests.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.Posts.Queries
{
    public class GetPostsByUserIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetPostsByUserIdQuery_SuccessOnFirstPage()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            (var page, var limit) = (1, 3);
            var handler = new GetPostsByUserIdQueryHandler(DbContext, Mapper);
            var expectedList = Mapper.Map<IEnumerable<PostVM>>(new List<Post>
            {
                AppDbContextFactory.PostE,
                AppDbContextFactory.PostD,
                AppDbContextFactory.PostC
            });

            //Act
            var resultList = await handler.Handle(
                new GetPostsByUserIdQuery(userId, page, limit),
                CancellationToken.None);

            //Assert
            Assert.NotNull(resultList);
            Assert.NotEmpty(resultList);
            resultList.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task GetPostsByUserIdQuery_SuccessOnSecondPage()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            (var page, var limit) = (2, 3);
            var handler = new GetPostsByUserIdQueryHandler(DbContext, Mapper);
            var expectedList = Mapper.Map<IEnumerable<PostVM>>(new List<Post>
            {
                AppDbContextFactory.PostB,
                AppDbContextFactory.PostA
            });

            //Act
            var resultList = await handler.Handle(
                new GetPostsByUserIdQuery(userId, page, limit),
                CancellationToken.None);

            //Assert
            Assert.NotNull(resultList);
            Assert.NotEmpty(resultList);
            resultList.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task GetPostsByUserIdQuery_SuccessOnWrongPage()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            (var page, var limit) = (10, 3);
            var handler = new GetPostsByUserIdQueryHandler(DbContext, Mapper);

            //Act
            var resultList = await handler.Handle(
                new GetPostsByUserIdQuery(userId, page, limit),
                CancellationToken.None);

            //Assert
            resultList.Should().BeEmpty();
        }
    }
}
