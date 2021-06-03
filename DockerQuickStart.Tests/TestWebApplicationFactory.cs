using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DockerQuickStart.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DockerQuickStart.Tests
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly string _connectionString = "Server=localhost,1433;Database=Blog;User=sa;Password=U4bT^3)ewQ";
        //private readonly string _connectionString = "Data Source=localhost; Initial Catalog=Blog;User=sa;Password=U4bT^3)ewQ";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, conf) =>
            {
                conf
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
            });

            builder.ConfigureServices(services =>
            {
                var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(BlogDbContext));
                services.Remove(dbContext);

                services.AddDbContext<BlogDbContext>((serviceProvider, options) =>
                {
                    var configuration = serviceProvider.GetService<IConfiguration>();
                    //options.UseSqlServer(configuration.GetConnectionString("BlogConnection"));
                    options.UseSqlServer(_connectionString);
                }, ServiceLifetime.Transient, ServiceLifetime.Singleton);

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<BlogDbContext>();
                    db.Database.EnsureDeleted();
                    db.Database.Migrate();
                }
            });

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new[] {
                    new KeyValuePair<string, string>("ConnectionStrings:BlogConnection", _connectionString)
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            var scope = Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<BlogDbContext>();
            db.Database.EnsureDeleted();

            base.Dispose(disposing);
        }
    }
}
