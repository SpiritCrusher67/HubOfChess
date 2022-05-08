namespace HubOfChess.Domain
{
    public class PostLike
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
