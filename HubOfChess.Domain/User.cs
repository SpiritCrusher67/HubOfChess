namespace HubOfChess.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string AboutMe { get; set; }
        public int XPCount { get; set; }
        public IEnumerable<User> Friends { get; set; }

    }
}
