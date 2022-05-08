using System;
using System.Threading;
using System.Threading.Tasks;
using HubOfChess.Application.Chats.Commands.CreateChat;
using HubOfChess.Application.Chats.Commands.DeleteChat;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HubOfChess.Tests.Chats.Commands
{
    public class DeleteChatCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteChatCommand_Success()
        {
            //Arrage
            var createHandler = new CreateChatCommandHandler(DbContext);
            var deleteHandler = new DeleteChatCommandHandler(DbContext);
            var userId = AppDbContextFactory.UserA.UserId;
            var chatId = await createHandler.Handle(new CreateChatCommand(userId), CancellationToken.None);

            //Act
            await deleteHandler.Handle(
                new DeleteChatCommand(chatId, userId), 
                CancellationToken.None);

            //Assert
            Assert.Null(await DbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId));
        }

        [Fact]
        public async Task DeleteChatCommand_FailOnWrongChatId()
        {
            //Arrage
            var deleteHandler = new DeleteChatCommandHandler(DbContext);
            var user = AppDbContextFactory.UserA;
            var chatId = Guid.NewGuid();

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
             deleteHandler.Handle(
                 new DeleteChatCommand(chatId, user.UserId),
                 CancellationToken.None));

        }

        [Fact]
        public async Task DeleteChatCommand_FailOnWrongUser()
        {
            //Arrage
            var createHandler = new CreateChatCommandHandler(DbContext);
            var deleteHandler = new DeleteChatCommandHandler(DbContext);
            var ownerUserId = AppDbContextFactory.UserA.UserId;
            var wrongUser = AppDbContextFactory.UserB;
            var chatId = await createHandler.Handle(new CreateChatCommand(ownerUserId), CancellationToken.None);

            //Act
            //Assert
            await Assert.ThrowsAsync<NoPermissionException>(() =>
             deleteHandler.Handle(
                 new DeleteChatCommand(chatId, wrongUser.UserId),
                 CancellationToken.None));
        }
    }
}
