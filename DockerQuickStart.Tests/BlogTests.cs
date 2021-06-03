using System;
using System.Threading.Tasks;
using DockerQuickStart.DataAccess;
using Microsoft.Data.SqlClient;
using Xunit;

namespace DockerQuickStart.Tests
{
    [Collection("Integration Tests")]
    public class BlogTests : IntegrationBase
    {
        public BlogTests(TestWebApplicationFactory<Startup> factory) 
            : base(factory)
        {
        }

        protected override string BaseUrl => "v1/Blogs";

        [Fact]
        public void Test1()
        {
            using (var connection = new SqlConnection("Server=localhost,1433;Database=Blog;User=sa;Password=U4bT^3)ewQ"))
            {
                connection.Open();
            }
        }

        //[Fact]
        //public void Test2()
        //{
        //    using (var connection = new SqlConnection("Server=localhost,1433;Database=Blog;User=sa;Password=U4bT^3)ewQ"))
        //    {
        //        connection.Open();
        //    }
        //}

        //[Fact]
        //public void Test3()
        //{

        //}

        //[Fact]
        //public void Test4()
        //{
        //    using (var connection = new SqlConnection("Server=localhost/sql-server-database,1433;Database=Blog;User=sa;Password=U4bT^3)ewQ"))
        //    {
        //        connection.Open();
        //    }
        //}

        [Fact]
        public async Task GetBlogById_Success()
        {
            // Arrange
            var id = new Guid("E852C0F8-0AD8-4DBE-B53B-9B10CD1E01F5");

            // Act
            var blog = await GetAsync<Blog>($"{BaseUrl}/{id}");

            // Assert
            Assert.NotNull(blog);
            Assert.Equal(id, blog.Id);
            Assert.Equal("https://blog1.com", blog.Url);
            Assert.Single(blog.Posts);
        }
    }
}
