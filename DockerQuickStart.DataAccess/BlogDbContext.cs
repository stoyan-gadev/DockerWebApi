using Microsoft.EntityFrameworkCore;

namespace DockerQuickStart.DataAccess
{
    public class BlogDbContext : DbContext
    {
        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Applies all the configurations for entities.	See	the	Configuration folder
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
