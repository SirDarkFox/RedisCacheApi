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
    public class GetFileByIdTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public GetFileByIdTests(IntegrationTestFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task GetById_ReturnsProperResponse()
        {
            var file = ModelGenerator.GenerateFile();

            using (var scope = _scopeFactory?.CreateScope())
            {
                var context = scope?.ServiceProvider.GetService<AppDbContext>();
                context?.Files.Add(file);
                context?.SaveChanges();
            }

            var response = await _client.GetAsync($"api/files/{file.Id}");
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(file.Id, result);
            Assert.Contains(file.Name, result);
        }
    }
}
