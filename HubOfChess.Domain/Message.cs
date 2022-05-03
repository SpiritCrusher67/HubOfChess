namespace HubOfChess.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public Chat Chat { get; set; }
        public User Sender { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

    }
}
