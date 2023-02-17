using RedisCacheApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedisCacheApi.Tests.FileController
{
    public class CreateFileTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public CreateFileTests(IntegrationTestFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task Create_ReturnsProperResponse()
        {
            var file = ModelGenerator.GenerateFile();

            var response = await _client.PostAsJsonAsync("api/files", file);
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.DoesNotContain(file.Id, result);
            Assert.Contains(file.Name, result);
        }

        [Fact]
        public async Task Create_ReturnsValidationError()
        {
            var file = ModelGenerator.GenerateFolder();
            file.Name = "fhibuysavbsyfbvsfjhvbahvbadjfkhvbdfksjhvbsdkjhfbvkdsjhbfv";

            var response = await _client.PostAsJsonAsync("api/files", file);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
