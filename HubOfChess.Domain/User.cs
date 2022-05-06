namespace HubOfChess.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? AboutMe { get; set; }
        public int XPCount { get; set; }
        public IEnumerable<UserFriend> Friends { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
        public IEnumerable<Chat> OwnedChats { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<PostLike> PostLikes { get; set; }
        public IEnumerable<PostComment> PostComments { get; set; }
        public IEnumerable<GameState> GamesPlayed { get; set; }
        public IEnumerable<GameState> WinningGames { get; set; }

    }
    public class UserFriend
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid FriendId { get; set; }
        public User Friend { get; set; }
    }
}
