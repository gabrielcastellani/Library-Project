using Library.Api.Domain.Abstracts;
using MediatR;

namespace Library.Api.Domain.Books.Commands
{
    internal record DeleteBook(Guid Id) : IRequest<Result>;
}
