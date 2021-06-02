using System;
using System.Threading.Tasks;
using DockerQuickStart.DataAccess;
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
