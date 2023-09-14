using LibraryBackend.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryBackend.Infrastructure.EntityTypeConfigurations;

public class BaseEntityTypeConfiguration<TEntity, EntityIdType> : IEntityTypeConfiguration<TEntity>
    where TEntity : AuditBaseEntity<EntityIdType>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
    }
}