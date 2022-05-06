using HubOfChess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubOfChess.Persistence.EntityTypeConfigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id);
            builder.HasMany(c => c.Users).WithMany(u => u.Chats);
        }
    }
}
