using Library.Api.Domain.Abstracts;
using MediatR;

namespace Library.Api.Domain.Books.Commands
{
    internal record DeleteBookCommand(Guid Id) : IRequest<Result>;
}
