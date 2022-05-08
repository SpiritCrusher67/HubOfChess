using FluentAssertions;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.PostComments.Queries.GetPostCommentById;
using HubOfChess.Application.ViewModels;
using HubOfChess.Tests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.PostComments.Queries
{
    public class GetPostCommentByIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetPostCommentByIdQuery_Success()
        {
            //Arrange
            var commentId = AppDbContextFactory.PostCommentA.Id;
            var handler = new GetPostCommentByIdQueryHandler(DbContext, Mapper);
            var expectedComment = Mapper.Map<PostCommentVM>(AppDbContextFactory.PostCommentA);

            //Act
            var comment = await handler.Handle(
                new GetPostCommentByIdQuery(commentId),
                CancellationToken.None);

            //Assert
            comment.Should().NotBeNull();
            comment.PostId.Should().Be(expectedComment.PostId);
            comment.UserId.Should().Be(expectedComment.UserId);
            comment.UserFullName.Should().Be(expectedComment.UserFullName);
            comment.Text.Should().Be(expectedComment.Text);
            comment.Date.Should().Be(expectedComment.Date);
        }

        [Fact]
        public async Task GetPostCommentByIdQuery_FailOnNotExistPostComment()
        {
            //Arrange
            var commentId = Guid.NewGuid();
            var handler = new GetPostCommentByIdQueryHandler(DbContext, Mapper);
            var expectedComment = Mapper.Map<PostCommentVM>(AppDbContextFactory.PostCommentA);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>( () =>
                handler.Handle(
                    new GetPostCommentByIdQuery(commentId),
                    CancellationToken.None));
        }
    }
}
