using dockerExampleWeb2.Models;
using Microsoft.EntityFrameworkCore;

internal class Database
{
    public static void MigrateDatabase(WebApplication app)
    {
        var dbContext = app.Services.GetService<SchoolContext>();
        var pendingMigration = dbContext!.Database.GetPendingMigrations();
        if (pendingMigration.Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
