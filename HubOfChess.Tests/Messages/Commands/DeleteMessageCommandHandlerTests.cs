using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Messages.Commands.DeleteMessage;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.Messages.Commands
{
    public class DeleteMessageCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteMessageCommand_Success()
        {
            //Arrange
            var userId = AppDbContextFactory.UserB.UserId;
            var msgId = AppDbContextFactory.MessageF.Id;
            var handler = new DeleteMessageCommandHandler(DbContext);

            //Act
            await handler.Handle(
                new DeleteMessageCommand(msgId, userId),
                CancellationToken.None);
            var msg = await DbContext.Messages.FirstOrDefaultAsync(m => m.Id == msgId);

            //Assert
            Assert.Null(msg);
        }

        [Fact]
        public async Task DeleteMessageCommand_FailOnNotExistingMessage()
        {
            //Arrange
            var userId = AppDbContextFactory.UserB.UserId;
            var msgId = Guid.NewGuid();
            var handler = new DeleteMessageCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(
               new DeleteMessageCommand(msgId, userId),
               CancellationToken.None));
        }

        [Fact]
        public async Task DeleteMessageCommand_FailOnNotExistingUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var msgId = AppDbContextFactory.MessageA.Id;
            var handler = new DeleteMessageCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(
               new DeleteMessageCommand(msgId, userId),
               CancellationToken.None));
        }

        [Fact]
        public async Task DeleteMessageCommand_FailOnWrongUser()
        {
            //Arrange
            var userId = AppDbContextFactory.UserB.UserId;
            var msgId = AppDbContextFactory.MessageA.Id;
            var handler = new DeleteMessageCommandHandler(DbContext);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
            handler.Handle(
               new DeleteMessageCommand(msgId, userId),
               CancellationToken.None));
        }

    }
}
