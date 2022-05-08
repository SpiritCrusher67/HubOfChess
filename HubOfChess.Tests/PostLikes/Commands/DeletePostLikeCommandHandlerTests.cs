using FluentAssertions;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.PostLikes.Commands.DeletePostLike;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.PostLikes.Commands
{
    public class DeletePostLikeCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeletePostLikeCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserB.UserId;
            var postId = AppDbContextFactory.PostA.Id;
            var handler = new DeletePostLikeCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new DeletePostLikeCommand(userId, postId),
                CancellationToken.None);
            var like = await DbContext.PostLikes
                .FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);

            //Assert
            like.Should().BeNull();
        }

        [Fact]
        public async Task DeletePostLikeCommand_FailOnNotExistingPostLike()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var postId = AppDbContextFactory.PostA.Id;
            var handler = new DeletePostLikeCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>( () =>
                handler.Handle(
                    new DeletePostLikeCommand(userId, postId),
                    CancellationToken.None));
        }
    }
}
