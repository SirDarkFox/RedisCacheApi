using Microsoft.Extensions.DependencyInjection;
using RedisCacheApi.Data;
using RedisCacheApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedisCacheApi.Tests.FolderController
{
    public class GetFolderByIdTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public GetFolderByIdTests(IntegrationTestFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task GetById_ReturnsProperResponse()
        {
            var folder = ModelGenerator.GenerateFolder();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Folders.Add(folder);
                context?.SaveChanges();
            }

            var response = await _client.GetAsync($"api/folders/{folder.Id}");
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(folder.Id, result);
            Assert.Contains(folder.Name, result);
        }
    }
}
