using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
{
    public BaseDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<BaseDbContext>();
        optionBuilder.UseSqlServer(ConnectionCofiguration.ConnectionString);
        return new BaseDbContext(optionBuilder.Options);
    }
}

