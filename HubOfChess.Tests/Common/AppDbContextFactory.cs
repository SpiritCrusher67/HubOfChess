using System;
using System.Collections.Generic;
using HubOfChess.Domain;
using HubOfChess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Tests.Common
{
    public static class AppDbContextFactory
    {
        public static readonly User UserA = new() { UserId = Guid.NewGuid(), FirstName = "UserA", LastName = "A" };
        public static readonly User UserB = new() { UserId = Guid.NewGuid(), FirstName = "UserB", LastName = "B" };
        public static readonly User UserC = new() { UserId = Guid.NewGuid(), FirstName = "UserC", LastName = "C" };

        public static readonly Chat ChatA = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserB }, Owner = UserA };
        public static readonly Chat ChatB = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserC }, Owner = UserC };
        public static readonly Chat ChatC = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserB, UserC }, Owner = UserA, Name = "TestChat C" };
        public static readonly Chat ChatD = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA }, Owner = UserA, Name = "TestChat D" };

        public static readonly Message MessageA = new()
        {
            Id = Guid.Parse("DA0EF73A-BB52-4E44-B24F-F241418FFA59"),
            Chat = ChatA,
            Sender = UserA,
            Text = "Test msg A"
        };
        public static readonly Message MessageB = new()
        {
            Id = Guid.Parse("112E2AD7-A9EE-4E6A-8FCC-A532A5EE846C"),
            Chat = ChatA,
            Sender = UserB,
            Text = "Test msg B"
        };
        public static readonly Message MessageC = new()
        {
            Id = Guid.Parse("3BB8D12F-C17A-42EB-B945-315559BB65E0"),
            Chat = ChatC,
            Sender = UserA,
            Text = "Test msg C"
        };
        public static readonly Message MessageD = new()
        {
            Id = Guid.Parse("7B301DDA-DEBA-433B-AE11-13380142B0BF"),
            Chat = ChatC,
            Sender = UserB,
            Text = "Test msg D"
        };
        public static readonly Message MessageF = new()
        {
            Id = Guid.Parse("7B301DDA-DEBA-433B-AE11-13380142B0BF"),
            Chat = ChatD,
            Sender = UserB,
            Text = "Test msg F"
        };

        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            context.Users.AddRange(UserA, UserB, UserC);
            context.Chats.AddRange(ChatA, ChatB, ChatC,ChatD);
            context.Messages.AddRange(
                MessageA, MessageB, MessageC,MessageD
            );

            context.SaveChanges();
            return context;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
