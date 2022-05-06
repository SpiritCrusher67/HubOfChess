using HubOfChess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubOfChess.Persistence.EntityTypeConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Author).WithMany(u => u.Posts);

        }
    }
    public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
    {
        public void Configure(EntityTypeBuilder<PostLike> builder)
        {
            builder.HasKey(l => new { l.PostId, l.UserId });
            builder.HasOne(l => l.Post).WithMany(u => u.Likes);
            builder.HasOne(l => l.User).WithMany(p => p.PostLikes);
        }
    }
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {

            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Post).WithMany(p => p.Comments);
            builder.HasOne(c => c.User).WithMany(u => u.PostComments);
        }
    }
}
