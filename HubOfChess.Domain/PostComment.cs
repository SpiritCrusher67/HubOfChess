namespace HubOfChess.Domain
{
    public class PostComment
    {
        public Guid Id { get; set; }
        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
