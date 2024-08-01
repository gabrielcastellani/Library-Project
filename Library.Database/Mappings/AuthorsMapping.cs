using Library.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Database.Mappings
{
    internal class AuthorsMapping : IEntityTypeConfiguration<Authors>
    {
        public void Configure(EntityTypeBuilder<Authors> builder)
        {
            builder.ToTable(nameof(Authors));
            builder.HasKey(author => author.Id);
            builder.Property(author => author.Id).IsRequired();
            builder.Property(author => author.FirstName).IsRequired();
            builder.Property(author => author.LastName).IsRequired();
            builder.Property(author => author.BirthDate).IsRequired();
            builder
                .HasMany(author => author.Books)
                .WithOne(book => book.Author)
                .HasForeignKey(book => book.AuthorId)
                .IsRequired(false);
        }
    }
}
