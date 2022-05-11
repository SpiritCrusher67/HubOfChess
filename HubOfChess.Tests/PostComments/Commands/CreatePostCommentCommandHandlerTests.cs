using FluentAssertions;
using HubOfChess.Application.PostComments.Commands.CreatePostComment;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.PostComments.Commands
{
    public class CreatePostCommentCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreatePostCommentCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var postId = AppDbContextFactory.PostC.Id;
            var text = "4FD8-A43F";
            var handler = new CreatePostCommentCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            var commentId = await handler.Handle(
                new CreatePostCommentCommand(userId, postId, text),
                CancellationToken.None);
            var comment = await DbContext.PostComments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            //Assert
            comment.Should().NotBeNull();
            comment!.User.UserId.Should().Be(userId);
            comment.Post.Id.Should().Be(postId);
            comment.Text.Should().Be(text);
        }
    }
}
