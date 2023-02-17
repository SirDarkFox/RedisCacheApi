using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
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

//Dependency Injection
builder.Services.AddScoped<ISqlFolderRepo, SqlServerFolderRepo>();
builder.Services.AddScoped<ISqlFileRepo, SqlServerFileRepo>();
builder.Services.AddScoped<INoSqlFileRepo, RedisFileRepo>();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Patch Request
builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    s.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

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
