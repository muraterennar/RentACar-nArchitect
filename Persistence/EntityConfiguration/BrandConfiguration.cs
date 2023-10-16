using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        // 'Brands' adında bir tabloya erişim sağlayan konfigürasyon
        builder.ToTable("Brands").HasKey(b => b.Id);

        // 'Id' sütununun adını 'Id' olarak ayarlar ve gereklidir (not null)
        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();

        // 'Name' sütununun adını 'Name' olarak ayarlar ve gereklidir (not null)
        builder.Property(b => b.Name).HasColumnName("Name").IsRequired();

        // 'CreatedDate' sütununun adını 'CreatedDate' olarak ayarlar ve gereklidir (not null)
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();

        // 'UpdatedDate' sütununun adını 'UpdatedDate' olarak ayarlar
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");

        // 'DeletedDate' sütununun adını 'DeletedDate' olarak ayarlar
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        // 'Name' sütunu üzerinde benzersiz bir indeks oluşturur
        builder.HasIndex(indexExpression: b => b.Name, name: "UK_Brands_Name").IsUnique();

        // 'Models' ilişkisi tanımlar
        builder.HasMany(b => b.Models);

        // Silinmiş kayıtları filtreler (DeletedDate değeri null olanları kabul eder)
        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }

}