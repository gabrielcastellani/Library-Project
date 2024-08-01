namespace Library.Database.Entities
{
    public class Authors
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Books> Books { get; set; } = new List<Books>();
    }
}
