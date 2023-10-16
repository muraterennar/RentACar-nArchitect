using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Transmission> Transmissions { get; set; }
    public DbSet<Fuel> Fuels { get; set; }

    public BaseDbContext()
    {

    }

    // BaseDbContext sınıfının yapıcı metodudur.
    // DbContextOptions türünden bir parametre alır.
    public BaseDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        // Veritabanını oluşturur veya mevcutsa sadece kontrol eder.
        Database.EnsureCreated();
    }

    // DbContext sınıfının miras alındığı OnModelCreating metodunu geçersiz kılar.
    // ModelBuilder nesnesi üzerinden veritabanı tablolarının yapılandırılmasını sağlar.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Bu metot, mevcut projenin yürütüldüğü derleme içerisindeki tüm Entity Configuration sınıflarını bulur
        // ve bu sınıfları kullanarak veritabanı tablolarının yapılandırılmasını yapar.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
