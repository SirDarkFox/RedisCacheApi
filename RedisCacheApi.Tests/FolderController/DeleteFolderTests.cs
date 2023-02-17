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
    public class DeleteFolderTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public DeleteFolderTests(IntegrationTestFactory factory)
           : base(factory)
        {

        }

        [Fact]
        public async Task Delete_ReturnsProperResponse()
        {
            var folder = ModelGenerator.GenerateFolder();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Folders.Add(folder);
                context?.SaveChanges();

                var response = await _client.DeleteAsync($"api/folders/{folder.Id}");
                var deletedFolder = context?.Folders.FirstOrDefault(f => f.Id == folder.Id);

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                Assert.Null(deletedFolder);
            }
        }
    }
}
