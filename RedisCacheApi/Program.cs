using Microsoft.EntityFrameworkCore;
using RedisCacheApi.Data;
using RedisCacheApi.Data.FileRepos;
using RedisCacheApi.Data.FolderRepos;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

//SQL Server Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies()
           .UseSqlServer(builder.Configuration["ConnectionStrings:SqlServerConnection"]));

//Redis Context
builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
    ConnectionMultiplexer.Connect(builder.Configuration["ConnectionStrings:RedisConnection"]));

builder.Services.AddControllers();

//Dependency Injection
builder.Services.AddScoped<ISqlFolderRepo, SqlServerFolderRepo>();
builder.Services.AddScoped<ISqlFileRepo, SqlServerFileRepo>();
builder.Services.AddScoped<INoSqlFileRepo, RedisFileRepo>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
