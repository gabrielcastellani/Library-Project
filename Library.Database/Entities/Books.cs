namespace Library.Database.Entities
{
    public class Books
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid AuthorId { get; set; }
        public Authors Author { get; set; } = null!;
    }
}
