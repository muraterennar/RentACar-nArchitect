using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        // "Models" adlı bir tabloyu oluştur ve bu tablonun anahtarını "Id" sütununa ata.
        builder.ToTable("Models").HasKey(m => m.Id);

        // "Id" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();

        // "BrandId" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.BrandId).HasColumnName("BrandId").IsRequired();

        // "FuelId" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.FuelId).HasColumnName("FuelId").IsRequired();

        // "TransmissionId" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.TransmissionId).HasColumnName("TranmissionId").IsRequired();

        // "Name" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.Name).HasColumnName("Name").IsRequired();

        // "DailyPrice" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.DailyPrice).HasColumnName("DailyPrice").IsRequired();

        // "ImageUrl" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.ImageUrl).HasColumnName("ImageUrl").IsRequired();

        // "CreatedDate" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();

        // "UpdatedDate" sütununu oluştur ve zorunlu bir alan yap.
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");

        // "DeletedDate" sütununu oluştur ve bu alanı zorunlu yapma.
        builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");

        // "Name" sütunu için "UK_Models_Name" adlı benzersiz bir indeks oluştur.
        builder.HasIndex(indexExpression: t => t.Name, name: "UK_Models_Name").IsUnique();

        // "Brand", "Fuel" ve "Transmission" nesneleriyle ilişkileri tanımla.
        builder.HasOne(m => m.Brand);
        builder.HasOne(m => m.Fuel);
        builder.HasOne(m => m.Transmission);

        // "Cars" koleksiyonu ile ilişkiyi tanımla.
        builder.HasMany(m => m.Cars);

        // Silinmiş tarih (DeletedDate) alanı boş olmayan kayıtları filtrele.
        builder.HasQueryFilter(m => !m.DeletedDate.HasValue);

    }
}
