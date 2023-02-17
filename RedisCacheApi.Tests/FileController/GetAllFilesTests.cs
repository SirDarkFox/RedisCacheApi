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
    public class GetAllFilesTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public GetAllFilesTests(IntegrationTestFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task GetAll_ReturnsProperResponse()
        {
            var file1 = ModelGenerator.GenerateFile();
            var file2 = ModelGenerator.GenerateFile();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Files.Add(file1);
                context?.Files.Add(file2);
                context?.SaveChanges();
            }

            var response = await _client.GetAsync($"api/files");
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(file1.Id, result);
            Assert.Contains(file1.Name, result);
            Assert.Contains(file2.Id, result);
            Assert.Contains(file2.Name, result);
        }
    }
}
