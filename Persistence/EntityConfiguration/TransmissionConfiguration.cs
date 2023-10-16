using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class TransmissionConfiguration : IEntityTypeConfiguration<Transmission>
{
    public void Configure(EntityTypeBuilder<Transmission> builder)
    {
        // "Transmissions" adlı bir tabloya ait anahtar belirleme
        builder.ToTable("Transmissions").HasKey(t => t.Id);

        // "Id" adlı sütunu ekler ve zorunlu yapar
        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();

        // "Name" adlı sütunu ekler ve zorunlu yapar
        builder.Property(t => t.Name).HasColumnName("Name").IsRequired();

        // "CreatedDate" adlı sütunu ekler ve zorunlu yapar
        builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate").IsRequired();

        // "UpdatedDate" adlı sütunu ekler
        builder.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

        // "DeletedDate" adlı sütunu ekler
        builder.Property(t => t.DeletedDate).HasColumnName("DeletedDate");

        // "Name" sütunu üzerinde benzersizlik endeksi oluşturur
        builder.HasIndex(indexExpression: t => t.Name, name: "UK_Transmissions_Name").IsUnique();

        // "Transmissions" ile ilişkili "Models" koleksiyonunu tanımlar
        builder.HasMany(t => t.Models);

        // Silinmemiş verileri filtreler
        builder.HasQueryFilter(t => !t.DeletedDate.HasValue);

    }
}
