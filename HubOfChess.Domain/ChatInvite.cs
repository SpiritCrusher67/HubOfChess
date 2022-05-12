namespace HubOfChess.Domain
{
    public class ChatInvite
    {
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
        public Guid InvitedUserId { get; set; }
        public User InvitedUser { get; set; } = null!;
        public Guid SenderUserId { get; set; }
        public User SenderUser { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? InviteMessage { get; set; }
    }
}
