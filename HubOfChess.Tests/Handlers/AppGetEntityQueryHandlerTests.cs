using FluentAssertions;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Common.Handlers;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using HubOfChess.Tests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.Handlers
{
    public class AppGetEntityQueryHandlerTests
    {
        private readonly AppGetEntityQueryHandler queryHandler;
        public AppGetEntityQueryHandlerTests()
        {
            queryHandler = new(AppDbContextFactory.Create());
        }

        #region IGetEntityQueryHandler<User>
        [Fact]
        public async Task GetUser_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            IGetEntityQueryHandler<User> handler = queryHandler;

            //Act
            var user = await handler.GetEntityByIdAsync(userId, CancellationToken.None);

            //Assert
            user.Should().NotBeNull();
            user!.FirstName.Should().Be(AppDbContextFactory.UserA.FirstName);
            user.LastName.Should().Be(AppDbContextFactory.UserA.LastName);
            user.BirthDate.Should().Be(AppDbContextFactory.UserA.BirthDate);
        }

        [Fact]
        public async Task GetUser_FailOnNotExistingUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            IGetEntityQueryHandler<User> handler = queryHandler;

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.GetEntityByIdAsync(userId, CancellationToken.None));
        }
        #endregion

        #region IGetEntityQueryHandler<Chat>
        [Fact]
        public async Task GetChat_Success()
        {
            //Arrange
            var chatId = AppDbContextFactory.ChatA.Id;
            IGetEntityQueryHandler<Chat> handler = queryHandler;

            //Act
            var chat = await handler.GetEntityByIdAsync(chatId, CancellationToken.None);

            //Assert
            chat.Should().NotBeNull();
            chat!.Name.Should().Be(AppDbContextFactory.ChatA.Name);
        }

        [Fact]
        public async Task GetChat_FailOnNotExistingChat()
        {
            //Arrange
            var chatId = Guid.NewGuid();
            IGetEntityQueryHandler<Chat> handler = queryHandler;

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.GetEntityByIdAsync(chatId, CancellationToken.None));
        }
        #endregion

        #region IGetEntityQueryHandler<Message>
        [Fact]
        public async Task GetMessage_Success()
        {
            //Arrange
            var messageId = AppDbContextFactory.MessageA.Id;
            IGetEntityQueryHandler<Message> handler = queryHandler;

            //Act
            var message = await handler.GetEntityByIdAsync(messageId, CancellationToken.None);

            //Assert
            message.Should().NotBeNull();
            message!.Text.Should().Be(AppDbContextFactory.MessageA.Text);
            message.Date.Should().Be(AppDbContextFactory.MessageA.Date);
        }

        [Fact]
        public async Task GetMessage_FailOnNotExistingMessage()
        {
            //Arrange
            var messageId = Guid.NewGuid();
            IGetEntityQueryHandler<Message> handler = queryHandler;

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.GetEntityByIdAsync(messageId, CancellationToken.None));
        }
        #endregion

        #region IGetEntityQueryHandler<Post>
        [Fact]
        public async Task GetPost_Success()
        {
            //Arrange
            var postId = AppDbContextFactory.PostA.Id;
            IGetEntityQueryHandler<Post> handler = queryHandler;

            //Act
            var post = await handler.GetEntityByIdAsync(postId, CancellationToken.None);

            //Assert
            post.Should().NotBeNull();
            post!.Text.Should().Be(AppDbContextFactory.PostA.Text);
            post.Title.Should().Be(AppDbContextFactory.PostA.Title);
            post.Date.Should().Be(AppDbContextFactory.PostA.Date);
        }

        [Fact]
        public async Task GetPost_FailOnNotExistingPost()
        {
            //Arrange
            var postId = Guid.NewGuid();
            IGetEntityQueryHandler<Post> handler = queryHandler;

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.GetEntityByIdAsync(postId, CancellationToken.None));
        }
        #endregion

        #region IGetEntityQueryHandler<PostComment>
        [Fact]
        public async Task GetPostComment_Success()
        {
            //Arrange
            var postId = AppDbContextFactory.PostCommentA.Id;
            IGetEntityQueryHandler<PostComment> handler = queryHandler;

            //Act
            var postComment = await handler.GetEntityByIdAsync(postId, CancellationToken.None);

            //Assert
            postComment.Should().NotBeNull();
            postComment!.Text.Should().Be(AppDbContextFactory.PostCommentA.Text);
            postComment.Date.Should().Be(AppDbContextFactory.PostCommentA.Date);
        }

        [Fact]
        public async Task GetPostComment_FailOnNotExistingPostComment()
        {
            //Arrange
            var commentId = Guid.NewGuid();
            IGetEntityQueryHandler<PostComment> handler = queryHandler;

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
               handler.GetEntityByIdAsync(commentId, CancellationToken.None));
        }
        #endregion

    }
}
