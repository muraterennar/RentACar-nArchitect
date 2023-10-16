using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
{
    public void Configure(EntityTypeBuilder<Fuel> builder)
    {
        // "Fuels" adında bir tabloyu oluşturur ve anahtar olarak "Id" sütununu kullanır.
        builder.ToTable("Fuels").HasKey(f => f.Id);

        // "Id" sütunu oluşturulur ve zorunlu hale getirilir.
        builder.Property(f => f.Id).HasColumnName("Id").IsRequired();

        // "Name" sütunu oluşturulur ve zorunlu hale getirilir.
        builder.Property(f => f.Name).HasColumnName("Name").IsRequired();

        // "CreatedDate" sütunu oluşturulur ve zorunlu hale getirilir.
        builder.Property(f => f.CreatedDate).HasColumnName("CreatedDate").IsRequired();

        // "UpdatedDate" sütunu oluşturulur.
        builder.Property(f => f.UpdatedDate).HasColumnName("UpdatedDate");

        // "DeletedDate" sütunu oluşturulur.
        builder.Property(f => f.DeletedDate).HasColumnName("DeletedDate");

        // "Name" sütunu için "UK_Fuels_Name" adında benzersiz bir indeks ekler.
        builder.HasIndex(indexExpression: f => f.Name, name: "UK_Fuels_Name").IsUnique();

        // "Fuels" tablosu ile "Models" arasında ilişki oluşturur.
        builder.HasMany(f => f.Models);

        // Silinmiş öğeleri filtrelemek için "DeletedDate" sütununa sahip öğeleri filtreler.
        builder.HasQueryFilter(f => !f.DeletedDate.HasValue);

    }
}
