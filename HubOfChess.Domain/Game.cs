namespace HubOfChess.Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public User WhiteSideUser { get; set; }
        public User BlackSideUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? MovesHistory { get; set; }

    }
}
