using FluentAssertions;
using HubOfChess.Application.Posts.Commands.DeletePost;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.Posts.Commands
{
    public class DeletePostCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeletePostCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var postId = AppDbContextFactory.PostB.Id;
            var handler = new DeletePostCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new DeletePostCommand(postId,userId),
                CancellationToken.None);
            var post = await DbContext.Posts
                .FirstOrDefaultAsync(p => p.Id == postId);

            //Assert
            post.Should().BeNull();
        }

        [Fact]
        public async Task DeletePostCommand_LikesCascadeDelete_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var postId = AppDbContextFactory.PostA.Id;
            var handler = new DeletePostCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new DeletePostCommand(postId, userId),
                CancellationToken.None);
            var likes = await DbContext.PostLikes
                .Where(p => p.PostId == postId)
                .ToListAsync();

            //Assert
            likes.Should().BeEmpty();
        }

        [Fact]
        public async Task DeletePostCommand_CommentsCascadeDelete_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var postId = AppDbContextFactory.PostC.Id;
            var handler = new DeletePostCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new DeletePostCommand(postId, userId),
                CancellationToken.None);
            var likes = await DbContext.PostComments
                .Where(p => p.Post.Id == postId)
                .ToListAsync();

            //Assert
            likes.Should().BeEmpty();
        }
    }
}
