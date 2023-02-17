using RedisCacheApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedisCacheApi.Tests.FolderController
{
    public class CreateFolderTests : ClassFixtureBase, IClassFixture<IntegrationTestFactory>
    {
        public CreateFolderTests(IntegrationTestFactory factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task Create_ReturnsProperResponse()
        {
            var folder = ModelGenerator.GenerateFolder();

            var response = await _client.PostAsJsonAsync("api/folders", folder);
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.DoesNotContain(folder.Id, result);
            Assert.Contains(folder.Name, result);
        }

        [Fact]
        public async Task Create_ReturnsValidationError()
        {
            var folder = ModelGenerator.GenerateFolder();
            folder.Name = "fhibuysavbsyfbvsfjhvbahvbadjfkhvbdfksjhvbsdkjhfbvkdsjhbfv";

            var response = await _client.PostAsJsonAsync("api/folders", folder);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
