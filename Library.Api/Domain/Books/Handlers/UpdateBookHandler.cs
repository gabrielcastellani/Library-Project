using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Commands;
using Library.Database.Context;
using MediatR;

namespace Library.Api.Domain.Books.Handlers
{
    internal class UpdateBookHandler : IRequestHandler<UpdateBook, Result>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<UpdateBookHandler> _logger;

        public UpdateBookHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<UpdateBookHandler>();
        }

        public async Task<Result> Handle(UpdateBook request, CancellationToken cancellationToken)
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

                book.Name = request.Name;
                book.Description = request.Description;
                book.ReleaseDate = request.ReleaseDate;
                book.AuthorId = request.AuthorId;

                await _libraryDbContext.SaveChangesAsync(cancellationToken);

                return Result.Ok();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failure on update a book");
                return Result.Fail(error);
            }
        }
    }
}
