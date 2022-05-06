using System;
using System.Collections.Generic;
using HubOfChess.Domain;
using HubOfChess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Tests.Common
{
    public static class AppDbContextFactory
    {
        public static readonly User UserA = new() { UserId = Guid.NewGuid() };
        public static readonly User UserB = new() { UserId = Guid.NewGuid() };
        public static readonly User UserC = new() { UserId = Guid.NewGuid() };

        public static readonly Chat ChatA = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserB } };
        public static readonly Chat ChatB = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserC } };
        public static readonly Chat ChatC = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserB, UserC }, Owner = UserA, Name = "TestChat" };

        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            context.Users.AddRange(UserA, UserB, UserC);
            context.Chats.AddRange(ChatA, ChatB, ChatC);
            context.Messages.AddRange(
                new Message() 
                    { 
                        Id = Guid.Parse("DA0EF73A-BB52-4E44-B24F-F241418FFA59"),
                        Chat = ChatA,
                        Sender = UserA
                    },
                new Message()
                {
                    Id = Guid.Parse("112E2AD7-A9EE-4E6A-8FCC-A532A5EE846C"),
                    Chat = ChatA,
                    Sender = UserB
                },
                new Message()
                {
                    Id = Guid.Parse("3BB8D12F-C17A-42EB-B945-315559BB65E0"),
                    Chat = ChatC,
                    Sender = UserA
                },
                new Message()
                {
                    Id = Guid.Parse("7B301DDA-DEBA-433B-AE11-13380142B0BF"),
                    Chat = ChatC,
                    Sender = UserB
                }
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
