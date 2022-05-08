namespace HubOfChess.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? AboutMe { get; set; }
        public int XPCount { get; set; }
        public ICollection<UserFriend> Friends { get; set; } = null!;
        public ICollection<Chat> Chats { get; set; } = null!;
        public ICollection<Chat> OwnedChats { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = null!;
        public ICollection<Post> Posts { get; set; } = null!;
        public ICollection<PostLike> PostLikes { get; set; } = null!;
        public ICollection<PostComment> PostComments { get; set; } = null!;
        public ICollection<GameState> GamesPlayed { get; set; } = null!;
        public ICollection<GameState> WinningGames { get; set; } = null!;

    }
}
