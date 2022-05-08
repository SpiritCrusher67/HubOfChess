using FluentAssertions;
using HubOfChess.Application.PostLikes.Commands;
using HubOfChess.Tests.Common;
using HubOfChess.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.PostLikes
{
    public class CreatePostLikeCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreatePostLikeCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var postId = AppDbContextFactory.PostC.Id;
            var handler = new CreatePostLikeCommandHandler(DbContext);

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
            var handler = new CreatePostLikeCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<AlreadyExistException>( () =>
                handler.Handle(
                    new CreatePostLikeCommand(userId, postId),
                    CancellationToken.None));
        }
    }
}
