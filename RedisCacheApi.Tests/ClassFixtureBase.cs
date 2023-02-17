using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedisCacheApi.Tests
{
    public class ClassFixtureBase
    {
        protected readonly HttpClient _client;
        protected readonly IServiceScopeFactory? _scopeFactory;
        public ClassFixtureBase(IntegrationTestFactory factory)
        {
            _client = factory.CreateClient();
            _scopeFactory = factory.Services.GetService<IServiceScopeFactory>();
        }
    }
}
