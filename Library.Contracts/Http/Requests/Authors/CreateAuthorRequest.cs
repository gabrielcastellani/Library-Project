using System.ComponentModel.DataAnnotations;

namespace Library.Contracts.Http.Requests.Authors
{
    public record CreateAuthorRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
