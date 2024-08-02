using System.ComponentModel.DataAnnotations;

namespace Library.Contracts.Http.Requests.Books
{
    public record CreateBookRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
    }
}
