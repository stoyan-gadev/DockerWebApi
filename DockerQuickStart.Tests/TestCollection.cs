using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DockerQuickStart.Tests
{
    [CollectionDefinition("Integration Tests")]
    public class TestCollection : ICollectionFixture<WebApplicationFactory<DockerQuickStart.Startup>>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}