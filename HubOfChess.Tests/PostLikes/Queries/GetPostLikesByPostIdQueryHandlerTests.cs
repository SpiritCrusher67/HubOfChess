using FluentAssertions;
using HubOfChess.Application.PostLikes.Queries.GetPostLikesByPostId;
using HubOfChess.Application.ViewModels;
using HubOfChess.Tests.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.PostLikes.Queries
{
    public class GetPostLikesByPostIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetPostLikesByPostIdQuery_Success()
        {
            //Arrange
            var postId = AppDbContextFactory.PostA.Id;
            var handler = new GetPostLikesByPostIdQueryHandler(DbContext, Mapper);
            var expectedList = Mapper.Map<IEnumerable<PostLikeVM>>( new[] {
                AppDbContextFactory.PostLikeA, 
                AppDbContextFactory.PostLikeB });

            //Act
            var result = await handler.Handle(
                new GetPostLikesByPostIdQuery(postId),
                CancellationToken.None);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task GetPostLikesByPostIdQuery_SuccessEmptyResult()
        {
            //Arrange
            var postId = AppDbContextFactory.PostB.Id;
            var handler = new GetPostLikesByPostIdQueryHandler(DbContext, Mapper);
            var expectedList = Mapper.Map<IEnumerable<PostLikeVM>>(new[] {
                AppDbContextFactory.PostLikeA,
                AppDbContextFactory.PostLikeB });

            //Act
            var result = await handler.Handle(
                new GetPostLikesByPostIdQuery(postId),
                CancellationToken.None);

            //Assert
            result.Should().BeEmpty();
        }
    }
}
