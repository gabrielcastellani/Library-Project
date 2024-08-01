namespace Library.Contracts.Http.Requests
{
    public record CreateAuthorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
