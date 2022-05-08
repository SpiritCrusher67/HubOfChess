namespace HubOfChess.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public User Author { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
        public ICollection<PostLike> Likes { get; set; } = null!;
        public ICollection<PostComment> Comments { get; set; } = null!;
    }
}
