namespace HubOfChess.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public User Author { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

    }
}
