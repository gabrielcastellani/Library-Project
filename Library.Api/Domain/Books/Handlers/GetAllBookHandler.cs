using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Aggregates;
using Library.Api.Domain.Books.Commands;
using Library.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Domain.Books.Handlers
{
    internal sealed class GetAllBookHandler : IRequestHandler<GetAllBook, Result<Book[]>>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<GetAllBookHandler> _logger;

        public GetAllBookHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<GetAllBookHandler>();
        }

        public async Task<Result<Book[]>> Handle(GetAllBook request, CancellationToken cancellationToken)
        {
            try
            {
                var books = await _libraryDbContext
                    .Set<Database.Entities.Books>()
                    .Where(item => request.AuthorId.HasValue
                        ? item.AuthorId == request.AuthorId : true)
                    .Select(item => new Book(item))
                    .ToArrayAsync(cancellationToken);

                return Result<Book[]>.Ok(books);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failure on get all books");
                return Result<Book[]>.Fail(error);
            }
        }
    }
}
