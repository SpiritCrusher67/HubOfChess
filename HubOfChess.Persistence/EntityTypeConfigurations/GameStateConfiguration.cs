using HubOfChess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubOfChess.Persistence.EntityTypeConfigurations
{
    public class GameStateConfiguration : IEntityTypeConfiguration<GameState>
    {
        public void Configure(EntityTypeBuilder<GameState> builder)
        {
            builder.HasKey(g => g.Id);
            builder.HasMany(g => g.Users).WithMany(u => u.GamesPlayed);
            builder.HasOne(g => g.WinnerUser).WithMany(u => u.WinningGames);
        }
    }
}
