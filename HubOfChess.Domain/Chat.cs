namespace HubOfChess.Domain
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public User? Owner { get; set; }
        public ICollection<User> Users { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = null!;
    }
}
