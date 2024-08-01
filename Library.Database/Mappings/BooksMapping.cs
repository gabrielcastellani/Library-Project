using Library.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Database.Mappings
{
    internal class BooksMapping : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.ToTable(nameof(Books));
            builder.HasKey(book => book.Id);
            builder.Property(book => book.Id).IsRequired();
            builder.Property(book => book.Name).IsRequired();
            builder.Property(book => book.Description).IsRequired(false);
            builder.Property(book => book.ReleaseDate).IsRequired();
            builder.HasOne(book => book.Author)
                .WithMany(author => author.Books)
                .HasForeignKey(book => book.AuthorId)
                .IsRequired(true);
        }
    }
}
