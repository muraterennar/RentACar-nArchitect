using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        // "Cars" adında bir tabloya sahip olan ve "Id" sütununu anahtar olarak kullanacak.
        builder.ToTable("Cars").HasKey(c => c.Id);

        // "Id" sütununu "Id" olarak adlandırır ve zorunlu kılar.
        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();

        // "ModelId" sütununu "ModelId" olarak adlandırır ve zorunlu kılar.
        builder.Property(c => c.ModelId).HasColumnName("ModelId").IsRequired();

        // "Kilometer" sütununu "Kilometer" olarak adlandırır ve zorunlu kılar.
        builder.Property(c => c.Kilometer).HasColumnName("Kilometer").IsRequired();

        // "CarState" sütununu "State" olarak adlandırır.
        builder.Property(c => c.CarState).HasColumnName("State");

        // "ModelYear" sütununu "ModelYear" olarak adlandırır.
        builder.Property(c => c.ModelYear).HasColumnName("ModelYear");

        // "CreatedDate" sütununu "CreatedDate" olarak adlandırır ve zorunlu kılar.
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();

        // "UpdatedDate" sütununu "UpdatedDate" olarak adlandırır.
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");

        // "DeletedDate" sütununu "DeletedDate" olarak adlandırır.
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        // "Model" nesnesi ile ilişkilendirme yapar.
        builder.HasOne(c => c.Model);

        // "DeletedDate" değeri null olmayan kayıtları filtreler.
        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);

    }
}
