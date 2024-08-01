using Library.Api.Domain.Abstracts;
using MediatR;

namespace Library.Api.Domain.Books.Commands
{
    internal record UpdateBook(
        Guid Id,
        string Name,
        string Description,
        DateTime ReleaseDate,
        Guid AuthorId)
        : IRequest<Result>;
}
