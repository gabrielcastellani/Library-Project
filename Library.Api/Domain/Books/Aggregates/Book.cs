using Library.Api.Domain.Books.Commands;

namespace Library.Api.Domain.Books.Aggregates
{
    internal class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid AuthorId { get; set; }

        public Book(CreateBook createBook)
        {
            Id = Guid.NewGuid();
            Name = createBook.Name;
            Description = createBook.Description;
            ReleaseDate = createBook.ReleaseDate;
            AuthorId = createBook.AuthorId;
        }

        public Book(Database.Entities.Books book)
        {
            Id = book.Id;
            Name = book.Name;
            Description = book.Description;
            ReleaseDate = book.ReleaseDate;
            AuthorId = book.AuthorId;
        }

        public Database.Entities.Books ToEntity()
        {
            return new Database.Entities.Books()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                ReleaseDate = ReleaseDate,
                AuthorId = AuthorId,
            };
        }
    }
}
