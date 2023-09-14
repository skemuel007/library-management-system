using LibraryBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryBackend.Infrastructure.EntityTypeConfigurations;

public class BookConfiguration : BaseEntityTypeConfiguration<Book, Guid>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);
        builder.HasOne<Category>(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => b.ISBN).IsUnique();
        builder.HasIndex(b => b.Title);
    }
}