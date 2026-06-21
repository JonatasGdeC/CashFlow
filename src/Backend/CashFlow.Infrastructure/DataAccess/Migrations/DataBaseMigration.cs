using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure.DataAccess.Migrations;

public static class DataBaseMigration
{
    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        using CashFlowDbContext dbContext = serviceProvider.GetRequiredService<CashFlowDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}