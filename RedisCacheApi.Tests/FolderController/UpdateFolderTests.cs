using Microsoft.Extensions.DependencyInjection;
using RedisCacheApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RedisCacheApi.Data;

namespace RedisCacheApi.Tests.FolderController
{
    public class UpdateFolderTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public UpdateFolderTests(IntegrationTestFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task Update_ReturnsProperResponse()
        {
            var folder = ModelGenerator.GenerateFolder();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Folders.Add(folder);
                context?.SaveChanges();

                folder.Name = "New Name";
                var response = await _client.PutAsJsonAsync($"api/folders/{folder.Id}", folder);
                var updatedFolder = context?.Folders.FirstOrDefault(f => f.Id == folder.Id);

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                Assert.Contains(folder.Id, updatedFolder?.Id);
                Assert.Contains(folder.Name, updatedFolder?.Name);
            }
        }

        [Fact]
        public async Task Update_ReturnsValidationError()
        {
            var folder = ModelGenerator.GenerateFolder();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Folders.Add(folder);
                context?.SaveChanges();
            }

            folder.Name = "asfsjadnf;asjnf;kjsafnglkjdfgjdngjdngkjdslgkjbdkfjbgskdljwrbg";
            var response = await _client.PutAsJsonAsync($"api/folders/{folder.Id}", folder);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
