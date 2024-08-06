using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Commands;
using Library.Database.Context;
using MediatR;

namespace Library.Api.Domain.Books.Handlers
{
    internal class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<DeleteBookCommandHandler> _logger;

        public DeleteBookCommandHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<DeleteBookCommandHandler>();
        }

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
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
