namespace HubOfChess.Domain
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public User? Owner { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
