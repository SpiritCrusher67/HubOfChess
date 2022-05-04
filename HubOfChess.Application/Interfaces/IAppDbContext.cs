using HubOfChess.Domain;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Chat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<PostLike> PostLikes { get; set; }
        DbSet<PostComment> PostComments { get; set; }
        DbSet<GameState> GameStates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
