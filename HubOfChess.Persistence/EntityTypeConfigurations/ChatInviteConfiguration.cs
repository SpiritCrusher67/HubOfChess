using HubOfChess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubOfChess.Persistence.EntityTypeConfigurations
{
    internal class ChatInviteConfiguration :  IEntityTypeConfiguration<ChatInvite>
    {
        public void Configure(EntityTypeBuilder<ChatInvite> builder)
        {
            builder.HasKey(i => new { i.ChatId, i.InvitedUserId });
            builder.HasOne(i => i.Chat).
                WithMany(c => c.Invites)
                .HasForeignKey(i => i.ChatId);
            builder.HasOne(i => i.InvitedUser)
                .WithMany(u => u.ChatInvites)
                .HasForeignKey(i => i.InvitedUserId);
            builder.HasOne(i => i.SenderUser)
                .WithMany(u => u.SendedChatInvites)
                .HasForeignKey(i => i.SenderUserId);
        }
    }
}
