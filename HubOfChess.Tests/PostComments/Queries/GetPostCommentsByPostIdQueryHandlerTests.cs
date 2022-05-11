using FluentAssertions;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.PostComments.Queries.GetPostCommentsByPostId;
using HubOfChess.Application.ViewModels;
using HubOfChess.Tests.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.PostComments.Queries
{
    public class GetPostCommentsByPostIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetPostCommentsByPostIdQuery_Success()
        {
            //Arrange
            var postId = AppDbContextFactory.PostA.Id;
            var handler = new GetPostCommentsByPostIdQueryHandler(QueryHandler, Mapper);
            var expectedCommentsList = Mapper.Map<IEnumerable<PostCommentVM>>(new [] 
            { 
                AppDbContextFactory.PostCommentA, 
                AppDbContextFactory.PostCommentB 
            });

            //Act
            var commentsList = await handler.Handle(
                new GetPostCommentsByPostIdQuery(postId),
                CancellationToken.None);

            //Assert
            commentsList.Should().NotBeNullOrEmpty();
            commentsList.Should().BeEquivalentTo(expectedCommentsList);
        }

        [Fact]
        public async Task GetPostCommentsByPostIdQuery_SuccessEmptyResult()
        {
            //Arrange
            var postId = AppDbContextFactory.PostB.Id;
            var handler = new GetPostCommentsByPostIdQueryHandler(QueryHandler, Mapper);

            //Act
            var commentsList = await handler.Handle(
                new GetPostCommentsByPostIdQuery(postId),
                CancellationToken.None);

            //Assert
            commentsList.Should().NotBeNull();
            commentsList.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPostCommentsByPostIdQuery_FailOnNotExistPost()
        {
            //Arrange
            var postId = Guid.NewGuid();
            var handler = new GetPostCommentsByPostIdQueryHandler(QueryHandler, Mapper);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    new GetPostCommentsByPostIdQuery(postId),
                    CancellationToken.None));
        }
    }
}
