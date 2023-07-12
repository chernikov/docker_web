using dockerExampleWeb2.Controllers;
using dockerExampleWeb2.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Microsoft.Extensions.Caching.Redis;
using dockerExampleWeb2.Options;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.DependencyInjection;
using Elastic.Transport;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Default");
builder.Services.AddDbContext<SchoolContext>(options =>
       options.UseSqlServer(connectionString));

var mongoConnectionString = configuration.GetConnectionString("Mongo");
builder.Services.AddScoped(c => new SchoolMongoContext(mongoConnectionString));



var redisCacheSettings = configuration.GetSection("RedisCacheSettings").Get<RedisCacheSettings>();
builder.Services.AddSingleton(redisCacheSettings!);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisCacheSettings!.ConnectionString;
    options.InstanceName = redisCacheSettings!.InstanceName;
});



var elasticSearchOptions = configuration.GetSection("ElasticSearchSettings").Get<ElasticSearchOptions>()!;

builder.Services.AddSingleton<ElasticSearchOptions>(elasticSearchOptions);


var app = builder.Build();


//Database.MigrateDatabase(app);
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
