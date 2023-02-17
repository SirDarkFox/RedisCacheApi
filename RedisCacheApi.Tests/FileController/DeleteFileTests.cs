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

namespace RedisCacheApi.Tests.FileController
{
    public class DeleteFileTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public DeleteFileTests(IntegrationTestFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task Delete_ReturnsProperResponse()
        {
            var file = ModelGenerator.GenerateFile();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Files.Add(file);
                context?.SaveChanges();

                var response = await _client.DeleteAsync($"api/files/{file.Id}");
                var deletedFolder = context?.Files.FirstOrDefault(f => f.Id == file.Id);

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                Assert.Null(deletedFolder);
            }
        }
    }
}
