using HubOfChess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubOfChess.Persistence.EntityTypeConfigurations
{
    public class FriendInviteConfiguration : IEntityTypeConfiguration<FriendInvite>
    {
        public void Configure(EntityTypeBuilder<FriendInvite> builder)
        {
            builder.HasKey(i => new { i.SenderUserId, i.InvitedUserId });
            builder.HasOne(i => i.InvitedUser)
                .WithMany(u => u.FriendInvites)
                .HasForeignKey(i => i.InvitedUserId);
            builder.HasOne(i => i.SenderUser)
                .WithMany(u => u.SendedFriendInvites)
                .HasForeignKey(i => i.SenderUserId);
        }
        
    }
}
