using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Commands;
using Library.Database.Context;
using MediatR;

namespace Library.Api.Domain.Books.Handlers
{
    internal class DeleteBookHandler : IRequestHandler<DeleteBook, Result>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<DeleteBookHandler> _logger;

        public DeleteBookHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<DeleteBookHandler>();
        }

        public async Task<Result> Handle(DeleteBook request, CancellationToken cancellationToken)
        {
            try
            {
                var book = _libraryDbContext
                    .Set<Database.Entities.Books>()
                    .FirstOrDefault(item => item.Id == request.Id);

                if (book == null)
                {
                    return Result.Fail("Book not found!");
                }

                _libraryDbContext.Remove(book);
                await _libraryDbContext.SaveChangesAsync(cancellationToken);

                return Result.Ok();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failure on delete a book");
                return Result.Fail(error);
            }
        }
    }
}
