﻿namespace HubOfChess.Domain
{
    public class GameState
    {
        public Guid Id { get; set; }
        public User? WinnerUser { get; set; }
        public IEnumerable<User> Users { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? MovesHistory { get; set; }

    }
}
