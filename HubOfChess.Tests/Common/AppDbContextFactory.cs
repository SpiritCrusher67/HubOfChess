using System;
using System.Collections.Generic;
using HubOfChess.Domain;
using HubOfChess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Tests.Common
{
    public static class AppDbContextFactory
    {
        #region Users
        public static readonly User UserA = new() { UserId = Guid.NewGuid(), FirstName = "UserA", LastName = "A" };
        public static readonly User UserB = new() { UserId = Guid.NewGuid(), FirstName = "UserB", LastName = "B" };
        public static readonly User UserC = new() { UserId = Guid.NewGuid(), FirstName = "UserC", LastName = "C" };
        #endregion

        #region Chats
        public static readonly Chat ChatA = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserB }, Owner = UserA };
        public static readonly Chat ChatB = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserC }, Owner = UserC };
        public static readonly Chat ChatC = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA, UserB, UserC }, Owner = UserA, Name = "TestChat C" };
        public static readonly Chat ChatD = new() { Id = Guid.NewGuid(), Users = new List<User> { UserA }, Owner = UserA, Name = "TestChat D" };
        #endregion

        #region Messages
        public static readonly Message MessageA = new()
        {
            Id = Guid.Parse("DA0EF73A-BB52-4E44-B24F-F241418FFA59"),
            Chat = ChatA,
            Sender = UserA,
            Text = "Test msg A",
            Date = DateTime.Parse("09/03/2022 03:21:48")
        };
        public static readonly Message MessageB = new()
        {
            Id = Guid.Parse("112E2AD7-A9EE-4E6A-8FCC-A532A5EE846C"),
            Chat = ChatA,
            Sender = UserB,
            Text = "Test msg B",
            Date = DateTime.Parse("10/03/2022 03:21:48")
        };
        public static readonly Message MessageC = new()
        {
            Id = Guid.Parse("3BB8D12F-C17A-42EB-B945-315559BB65E0"),
            Chat = ChatA,
            Sender = UserA,
            Text = "Test msg C",
            Date = DateTime.Parse("11/03/2022 03:21:48")
        };
        public static readonly Message MessageD = new()
        {
            Id = Guid.Parse("7B301DDA-DEBA-433B-AE11-13380142B0BF"),
            Chat = ChatA,
            Sender = UserB,
            Text = "Test msg D",
            Date = DateTime.Parse("12/03/2022 03:21:48")
        };
        public static readonly Message MessageE = new()
        {
            Id = Guid.Parse("8778DCE5-19B0-4C36-A71C-FB91802DBCAB"),
            Chat = ChatA,
            Sender = UserB,
            Text = "Test msg F",
            Date = DateTime.Parse("12/03/2022 03:21:50")
        };
        #endregion

        #region Posts
        public static readonly Post PostA = new()
        {
            Id = Guid.Parse("A2DA86A2-6FCE-4E4E-8D67-6DA5B24C58C8"),
            Author = UserA,
            Title = "T Post A",
            Text = "Test post A",
            Date = DateTime.Parse("09/04/2022 03:21:48")
        };
        public static readonly Post PostB = new()
        {
            Id = Guid.Parse("AFBDD960-AD89-4CB4-B251-8E1A9E322F8D"),
            Author = UserA,
            Title = "T Post B",
            Text = "Test post B",
            Date = DateTime.Parse("10/04/2022 03:21:48")
        };
        public static readonly Post PostC = new()
        {
            Id = Guid.Parse("93393D7F-9670-4A3B-B2A3-804B22481F7F"),
            Author = UserA,
            Title = "T Post C",
            Text = "Test post C",
            Date = DateTime.Parse("11/04/2022 03:21:48")
        };
        public static readonly Post PostD = new()
        {
            Id = Guid.Parse("3BB02E1E-F50F-418A-B7FC-E07DE007FC88"),
            Author = UserA,
            Title = "T Post D",
            Text = "Test post D",
            Date = DateTime.Parse("12/04/2022 03:21:30")
        };
        public static readonly Post PostE = new()
        {
            Id = Guid.Parse("8747A0D0-DF67-4CE4-AAF7-4B8F6FC09A61"),
            Author = UserA,
            Title = "T Post E",
            Text = "Test post E",
            Date = DateTime.Parse("12/04/2022 03:21:48")
        };
        public static readonly Post PostF = new()
        {
            Id = Guid.Parse("8B7D291C-2B10-4A11-A11F-F7C66D26E020"),
            Author = UserB,
            Title = "T Post F",
            Text = "Test post F",
            Date = DateTime.Parse("09/04/2022 03:21:48")
        };
        #endregion

        #region PostLikes
        public static readonly PostLike PostLikeA = new()
        {
            Post = PostA,
            User = UserB,
            Date = DateTime.Parse("14/03/2022 03:21:48")
        };
        public static readonly PostLike PostLikeB = new()
        {
            Post = PostA,
            User = UserC,
            Date = DateTime.Parse("12/03/2022 04:12:32")
        };
        public static readonly PostLike PostLikeC = new()
        {
            Post = PostF,
            User = UserC,
            Date = DateTime.Parse("19/03/2022 05:32:12")

        };
        #endregion

        #region PostComments
        public static readonly PostComment PostCommentA = new()
        {
            Id = Guid.Parse("0B1DF306-5500-4384-83A9-B823A8C9081D"),
            Post = PostA,
            User = UserB,
            Text = "Test comment A",
            Date = DateTime.Now
        };
        public static readonly PostComment PostCommentB = new()
        {
            Id = Guid.Parse("F8E940C7-917F-4FB7-8C0B-F8B7474B65AB"),
            Post = PostA,
            User = UserC,
            Text = "Test comment B",
            Date = DateTime.Now
        };
        public static readonly PostComment PostCommentC = new()
        {
            Id = Guid.Parse("AED28403-08AA-42C8-9AA5-EC82A75FC9CF"),
            Post = PostC,
            User = UserA,
            Text = "Test comment C",
            Date = DateTime.Now
        };
        #endregion

        #region ChatInvites
        public static readonly ChatInvite ChatInviteA = new()
        {
            Chat = ChatD,
            SenderUser = UserA,
            ChatId = ChatD.Id,
            InvitedUser = UserB,
            InvitedUserId = UserB.UserId,
            Date = DateTime.Parse("10/02/2022 02:11:42")
        };
        public static readonly ChatInvite ChatInviteB = new()
        {
            Chat = ChatD,
            SenderUser = UserA,
            ChatId = ChatD.Id,
            InvitedUser = UserC,
            InvitedUserId = UserC.UserId,
            Date = DateTime.Parse("10/02/2022 02:21:42")
        };
        public static readonly ChatInvite ChatInviteC = new()
        {
            Chat = ChatA,
            SenderUser = UserA,
            ChatId = ChatA.Id,
            InvitedUser = UserC,
            InvitedUserId = UserC.UserId,
            Date = DateTime.Parse("10/02/2022 02:22:42")
        };
        #endregion

        #region FriendInvites
        public static readonly FriendInvite FriendInviteA = new()
        {
            SenderUser = UserA,
            SenderUserId = UserA.UserId,
            InvitedUser = UserB,
            InvitedUserId = UserB.UserId,
            Date = DateTime.Parse("10/02/2022 02:11:42")
        };
        public static readonly FriendInvite FriendInviteB = new()
        {
            SenderUser = UserC,
            SenderUserId = UserC.UserId,
            InvitedUser = UserB,
            InvitedUserId = UserB.UserId,
            Date = DateTime.Parse("12/02/2022 02:11:42")
        };
        #endregion
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
                MessageA, MessageB, MessageC, MessageD, MessageE);
            context.Posts.AddRange(
                PostA,PostB,PostC,PostD, PostE, PostF);
            context.PostLikes.AddRange(PostLikeA, PostLikeB, PostLikeC);
            context.PostComments.AddRange(
                PostCommentA, PostCommentB, PostCommentC);
            context.ChatInvites.AddRange(ChatInviteA, ChatInviteB, ChatInviteC);
            context.FriendInvites.AddRange(FriendInviteA, FriendInviteB);

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
