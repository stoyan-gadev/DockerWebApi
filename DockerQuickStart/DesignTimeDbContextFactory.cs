using System.IO;
using DockerQuickStart.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DockerQuickStart
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            // Load	the	settings from the project which	contains the connection	string
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<BlogDbContext>();

            builder.UseSqlServer(configuration.GetConnectionString("BlogConnection"));
            return new BlogDbContext(builder.Options);
        }
    }
}
