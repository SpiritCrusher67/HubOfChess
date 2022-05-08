using FluentAssertions;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.PostComments.Commands.DeletePostComment;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.PostComments.Commands
{
    public class DeletePostCommentCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeletePostCommentCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var commentId = AppDbContextFactory.PostCommentC.Id;
            var handler = new DeletePostCommentCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new DeletePostCommentCommand(userId, commentId),
                CancellationToken.None);
            var comment = await DbContext.PostComments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            //Assert
            comment.Should().BeNull();
        }

        [Fact]
        public async Task DeletePostCommentCommand_FailOnWrongUser()
        {
            //Arrange
            var userId = AppDbContextFactory.UserA.UserId;
            var commentId = AppDbContextFactory.PostCommentA.Id;
            var handler = new DeletePostCommentCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>( () =>
                handler.Handle(
                    new DeletePostCommentCommand(userId, commentId),
                    CancellationToken.None));
        }
    }
}
