namespace HubOfChess.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public Chat Chat { get; set; } = null!;
        public User Sender { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }

    }
}
