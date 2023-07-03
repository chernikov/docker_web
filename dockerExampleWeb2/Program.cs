using dockerExampleWeb2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Default");
builder.Services.AddDbContext<SchoolContext>(options =>
       options.UseSqlServer(connectionString));


var app = builder.Build();


//Database.MigrateDatabase(app);
using (var container = app.Services.CreateScope())
{
    var dbContext = container.ServiceProvider.GetService<SchoolContext>();
    var pendingMigration = dbContext!.Database.GetPendingMigrations();
    if (pendingMigration.Any())
    {
        dbContext.Database.Migrate();
    }
}


// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
