using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
{
    public BaseDbContext CreateDbContext(string[] args)
    {
        // DbContextOptionsBuilder sınıfı kullanılarak bir seçenek oluşturucu oluşturuluyor.
        var optionBuilder = new DbContextOptionsBuilder<BaseDbContext>();

        // Seçenek oluşturucuya, ConnectionCofiguration.ConnectionString ile belirtilen SQL Server bağlantı dizesini kullanması için talimat veriliyor.
        optionBuilder.UseSqlServer(ConnectionCofiguration.ConnectionString);

        // Yeni bir BaseDbContext örneği, oluşturulan seçeneklerle birlikte dönülüyor.
        return new BaseDbContext(optionBuilder.Options);
    }

}

