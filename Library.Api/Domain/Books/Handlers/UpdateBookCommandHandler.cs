using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Commands;
using Library.Database.Context;
using MediatR;

namespace Library.Api.Domain.Books.Handlers
{
    internal class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<UpdateBookCommandHandler> _logger;

        public UpdateBookCommandHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<UpdateBookCommandHandler>();
        }

        public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = _libraryDbContext
                    .Set<Database.Entities.Authors>()
                    .FirstOrDefault(item => item.Id == request.AuthorId);

                if (author == null)
                {
                    return Result.Fail("Author not found!");
                }

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
