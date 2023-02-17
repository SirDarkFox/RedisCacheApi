using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RedisCacheApi.Data;
using RedisCacheApi.Utility;
using StackExchange.Redis;
using Xunit;

namespace RedisCacheApi.Tests
{
    internal class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlTestcontainer _sqlContainer;
        private readonly RedisTestcontainer _redisContainer;

        public IntegrationTestFactory()
        {
            _sqlContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(new MsSqlTestcontainerConfiguration
                {
                    Password = "S3cur3P@ssW0rd!",
                    Database = "TestDb"
                })
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithCleanUp(true)
                .Build();

            _redisContainer = new TestcontainersBuilder<RedisTestcontainer>()
                .WithDatabase(new RedisTestcontainerConfiguration())
                .WithImage("redis:latest")
                .WithCleanUp(true)
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var con = _redisContainer.ConnectionString;

            builder.ConfigureTestServices(services =>
            {
                services.RemoveDbContext<AppDbContext>();
                services.RemoveAll<IConnectionMultiplexer>();

                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(string.Concat(_sqlContainer.ConnectionString, "TrustServerCertificate=True;"),
                        provider => provider.EnableRetryOnFailure()));
                services.AddSingleton<IConnectionMultiplexer>(opt =>
                    ConnectionMultiplexer.Connect(_redisContainer.ConnectionString));

                services.EnsureDbCreated<AppDbContext>();
            });
        }

        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();
            await _redisContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _sqlContainer.DisposeAsync();
            await _redisContainer.DisposeAsync();
        }
    }
}
