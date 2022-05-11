namespace HubOfChess.Domain
{
    public class FriendInvite
    {
        public Guid InvitedUserId { get; set; }
        public User InvitedUser { get; set; } = null!;
        public Guid SenderUserId { get; set; }
        public User SenderUser { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? InviteMessage { get; set; }
    }
}
