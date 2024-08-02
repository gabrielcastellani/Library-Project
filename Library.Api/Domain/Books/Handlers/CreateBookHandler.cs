using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Aggregates;
using Library.Api.Domain.Books.Commands;
using Library.Database.Context;
using MediatR;

namespace Library.Api.Domain.Books.Handlers
{
    internal class CreateBookHandler : IRequestHandler<CreateBook, Result<Book>>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<CreateBookHandler> _logger;

        public CreateBookHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<CreateBookHandler>();
        }

        public async Task<Result<Book>> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            try
            {
                var author = _libraryDbContext
                    .Set<Database.Entities.Authors>()
                    .FirstOrDefault(item => item.Id == request.AuthorId);

                if (author == null)
                {
                    return Result<Book>.Fail("Author not found!");
                }

                var book = new Book(request);

                _libraryDbContext
                    .Set<Database.Entities.Books>()
                    .Add(book.ToEntity());

                await _libraryDbContext.SaveChangesAsync(cancellationToken);

                return Result<Book>.Ok(book);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failure on create a new book");
                return Result<Book>.Fail(error);
            }
        }
    }
}
