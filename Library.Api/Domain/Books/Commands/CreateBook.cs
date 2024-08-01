using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Aggregates;
using MediatR;

namespace Library.Api.Domain.Books.Commands
{
    internal record CreateBook(
        string Name,
        string Description,
        DateTime ReleaseDate,
        Guid AuthorId)
        : IRequest<Result<Book>>;
}
