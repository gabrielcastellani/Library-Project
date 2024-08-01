using Library.Database.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Library.Database.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorsMapping());
            modelBuilder.ApplyConfiguration(new BooksMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
