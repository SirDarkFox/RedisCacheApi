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
    public class GetAllFoldersTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public GetAllFoldersTests(IntegrationTestFactory factory)
           : base(factory)
        {

        }

        [Fact]
        public async Task GetAll_ReturnsProperResponse()
        {
            var folder1 = ModelGenerator.GenerateFolder();
            var folder2 = ModelGenerator.GenerateFolder();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Folders.Add(folder1);
                context?.Folders.Add(folder2);
                context?.SaveChanges();
            }

            var response = await _client.GetAsync($"api/folders");
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(folder1.Id, result);
            Assert.Contains(folder1.Name, result);
            Assert.Contains(folder2.Id, result);
            Assert.Contains(folder2.Name, result);
        }
    }
}
