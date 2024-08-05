using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Aggregates;
using Library.Api.Domain.Books.Commands;
using Library.Api.Domain.Books.Services.Interfaces;
using MediatR;

namespace Library.Api.Domain.Books.Services
{
    internal sealed class BooksService : IBooksService
    {
        private readonly IMediator _mediator;

        public BooksService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Result<Book[]>> GetAll(Guid? authorId = null)
        {
            return _mediator.Send(new GetAllBook
            {
                AuthorId = authorId,
            });
        }

        public Task<Result<Book>> Create(Book book)
        {
            return _mediator.Send(
                new CreateBook(
                    Name: book.Name,
                    Description: book.Description,
                    ReleaseDate: book.ReleaseDate,
                    AuthorId: book.AuthorId));
        }

        public Task<Result> Update(Book book)
        {
            return _mediator.Send(
                new UpdateBook(
                    Id: book.Id,
                    Name: book.Name,
                    Description: book.Description,
                    ReleaseDate: book.ReleaseDate,
                    AuthorId: book.AuthorId));
        }

        public Task<Result> Delete(Guid bookId)
        {
            return _mediator.Send(new DeleteBook(bookId));
        }
    }
}
