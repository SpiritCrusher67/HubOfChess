using HubOfChess.Application.Posts.Commands.CreatePost;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System;
using HubOfChess.Application.Common.Exceptions;

namespace HubOfChess.Tests.Posts.Commands
{
    public class CreatePostCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreatePostCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = AppDbContextFactory.ChatA.Id;
            var title = "AA51B3EA3282";
            var text = "4AB4-8A69-381CD14A7421";
            var handler = new CreatePostCommandHandler(DbContext);

            //Act
            var postId = await handler.Handle(
                new CreatePostCommand(userId, title,text),
                CancellationToken.None);
            var post = await DbContext.Posts
                .FirstOrDefaultAsync(p => p.Id == postId);

            //Assert
            post.Should().NotBeNull();
            post!.Author.UserId.Should().Be(userId);
            post.Title.Should().Be(title);
            post.Text.Should().Be(text);
        }

        [Fact]
        public async Task CreatePostCommand_FailOnNotExistingUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var chatId = AppDbContextFactory.ChatA.Id;
            var title = "AA51B3EA3282";
            var text = "4AB4-8A69-381CD14A7421";
            var handler = new CreatePostCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>( () =>
                handler.Handle(
                    new CreatePostCommand(userId, title, text),
                    CancellationToken.None));
        }
    }
}
