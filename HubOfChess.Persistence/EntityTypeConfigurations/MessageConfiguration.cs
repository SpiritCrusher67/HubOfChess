using HubOfChess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubOfChess.Persistence.EntityTypeConfigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Chat).WithMany(c => c.Messages);
            builder.HasOne(m => m.Sender).WithMany(u => u .Messages);

        }
    }
}
