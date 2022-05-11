using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using HubOfChess.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Persistence
{
    public sealed class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<PostLike> PostLikes { get; set; } = null!;
        public DbSet<PostComment> PostComments { get; set; } = null!;
        public DbSet<GameState> GameStates { get; set; } = null!;
        public DbSet<ChatInvite> ChatInvites { get; set; } = null!;
        public DbSet<FriendInvite> FriendInvites { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserFriendConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostLikeConfiguration());
            modelBuilder.ApplyConfiguration(new PostCommentConfiguration());
            modelBuilder.ApplyConfiguration(new GameStateConfiguration());
            modelBuilder.ApplyConfiguration(new ChatInviteConfiguration());
            modelBuilder.ApplyConfiguration(new FriendInviteConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
