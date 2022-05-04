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
            builder.HasOne(p => p.Author).WithMany("Posts");

        }
    }
    public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
    {
        public void Configure(EntityTypeBuilder<PostLike> builder)
        {
            builder.HasKey(l => new { l.Post, l.User });
            builder.HasOne(l => l.Post).WithMany("Likes");
            builder.HasOne(l => l.User).WithMany("PostsLikes");
        }
    }
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {

            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Post).WithMany("Comments");
            builder.HasOne(c => c.User).WithMany("PostsComments");
        }
    }
}
