using dockerExampleWeb2.Models;
using Microsoft.EntityFrameworkCore;

internal class Database
{
    public static void MigrateDatabase(WebApplication app)
    {
        using (var container = app.Services.CreateScope())
        {
            var dbContext = container.ServiceProvider.GetService<SchoolContext>();
            var pendingMigration = dbContext!.Database.GetPendingMigrations();
            if (pendingMigration.Any())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
