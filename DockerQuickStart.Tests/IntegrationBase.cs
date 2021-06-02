using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DockerQuickStart.DataAccess;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DockerQuickStart.Tests
{
    public abstract class IntegrationBase : IClassFixture<TestWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        private readonly IDbContextTransaction _dbContextTransaction;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly BlogDbContext _blogDbContext;

        protected abstract string BaseUrl { get; }

        protected HttpClient Client { get; }

        public IntegrationBase(TestWebApplicationFactory<Startup> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _serviceScope = factory.Services.CreateScope();
            _blogDbContext = _serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
            _dbContextTransaction = _blogDbContext.Database.BeginTransaction();

            Client = factory.CreateClient();
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };
        }

        protected async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream, _jsonSerializerOptions);
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await Deserialize<T>(response);
        }

        public void Dispose()
        {
            _dbContextTransaction?.Rollback();
            _serviceScope?.Dispose();
        }
    }
}
