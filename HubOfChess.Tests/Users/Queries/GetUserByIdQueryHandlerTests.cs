using FluentAssertions;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Users.Queries.GetUserById;
using HubOfChess.Tests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HubOfChess.Tests.Users.Queries
{
    public class GetUserByIdQueryHandlerTests : TestQueryBase
    {
        [Fact]
        public async Task GetUserByIdQuery_Success()
        {
            //Arrange
            var user = AppDbContextFactory.UserA;
            var handler = new GetUserByIdQueryHandler(DbContext, Mapper);

            //Act
            var result = await handler.Handle(
                new GetUserByIdQuery(user.UserId),
                CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.UserId);
            result.AboutMe.Should().Be(user.AboutMe);
            result.BirthDate.Should().Be(user.BirthDate);
            result.FirstName.Should().Be(user.FirstName);
            result.LastName.Should().Be(user.LastName);
        }

        [Fact]
        public async Task GetUserByIdQuery_FailOnNotExistingUser()
        {
            //Arrange
            var user = Guid.NewGuid();
            var handler = new GetUserByIdQueryHandler(DbContext, Mapper);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                new GetUserByIdQuery(user),
                CancellationToken.None));
        }
    }
}
