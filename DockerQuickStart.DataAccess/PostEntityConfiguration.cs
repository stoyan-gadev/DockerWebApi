using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DockerQuickStart.DataAccess
{
    public sealed class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).HasMaxLength(255);

            builder
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId)
                .Metadata.DependentToPrincipal.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
