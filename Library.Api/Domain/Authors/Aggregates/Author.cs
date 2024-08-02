using Library.Api.Domain.Authors.Commands;

namespace Library.Api.Domain.Authors.Aggregates
{
    internal class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public Author() { }

        public Author(CreateAuthor createAuthor)
        {
            Id = Guid.NewGuid();
            FirstName = createAuthor.FirstName;
            LastName = createAuthor.LastName;
            BirthDate = createAuthor.BirthDate;
        }

        public Author(Database.Entities.Authors author)
        {
            Id = author.Id;
            FirstName = author.FirstName;
            LastName = author.LastName;
            BirthDate = author.BirthDate;
        }

        public Database.Entities.Authors ToEntity()
        {
            return new Database.Entities.Authors
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate
            };
        }
    }
}
