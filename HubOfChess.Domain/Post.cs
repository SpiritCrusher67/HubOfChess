namespace HubOfChess.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public User Author { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<PostLike> Likes { get; set; }
        public IEnumerable<PostComment> Comments { get; set; }
    }

    public class PostLike
    {
        public Post Post{ get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }

    public class PostComment
    {
        public Guid Id { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
