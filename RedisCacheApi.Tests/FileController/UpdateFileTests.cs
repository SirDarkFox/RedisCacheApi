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

namespace RedisCacheApi.Tests.FileController
{
    public class UpdateFileTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public UpdateFileTests(IntegrationTestFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task Update_ReturnsProperResponse()
        {
            var file = ModelGenerator.GenerateFile();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Files.Add(file);
                context?.SaveChanges();

                file.Name = "New Name";
                var response = await _client.PutAsJsonAsync($"api/files/{file.Id}", file);
                var updatedFile = context?.Files.FirstOrDefault(f => f.Id == file.Id);

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                Assert.Contains(file.Id, updatedFile?.Id);
                Assert.Contains(file.Name, updatedFile?.Name);
            }
        }

        [Fact]
        public async Task Update_ReturnsValidationError()
        {
            var file = ModelGenerator.GenerateFile();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Files.Add(file);
                context?.SaveChanges();
            }

            file.Name = "asfsjadnf;asjnf;kjsafnglkjdfgjdngjdngkjdslgkjbdkfjbgskdljwrbg";
            var response = await _client.PutAsJsonAsync($"api/files/{file.Id}", file);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
