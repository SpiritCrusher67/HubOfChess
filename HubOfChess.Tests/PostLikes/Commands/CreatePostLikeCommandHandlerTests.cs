using FluentAssertions;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using HubOfChess.Application.PostLikes.Commands.CreatePostLike;

namespace HubOfChess.Tests.PostLikes.Commands
{
    public class CreatePostLikeCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreatePostLikeCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserC.UserId;
            var postId = AppDbContextFactory.PostC.Id;
            var handler = new CreatePostLikeCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            await handler.Handle(
                new CreatePostLikeCommand(userId, postId),
                CancellationToken.None);
            var like = await DbContext.PostLikes
                .FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);

            //Assert
            like.Should().NotBeNull();
            like!.UserId.Should().Be(userId);
            like.PostId.Should().Be(postId);
        }

        [Fact]
        public async Task CreatePostLikeCommand_FailOnAddingExistingPostLike()
        {
            //Arrange
            var userId = AppDbContextFactory.UserB.UserId;
            var postId = AppDbContextFactory.PostA.Id;
            var handler = new CreatePostLikeCommandHandler(DbContext, QueryHandler, QueryHandler);

            //Act
            //Assert
            await Assert.ThrowsAsync<AlreadyExistException>( () =>
                handler.Handle(
                    new CreatePostLikeCommand(userId, postId),
                    CancellationToken.None));
        }
    }
}
