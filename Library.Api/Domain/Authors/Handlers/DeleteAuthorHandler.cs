using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Commands;
using Library.Database.Context;
using MediatR;

namespace Library.Api.Domain.Authors.Handlers
{
    internal class DeleteAuthorHandler : IRequestHandler<DeleteAuthor, Result>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<DeleteAuthorHandler> _logger;

        public DeleteAuthorHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<DeleteAuthorHandler>();
        }

        public async Task<Result> Handle(DeleteAuthor request, CancellationToken cancellationToken)
        {
            try
            {
                var author = _libraryDbContext
                    .Set<Database.Entities.Authors>()
                    .FirstOrDefault(item => item.Id == request.Id);

                if (author == null)
                {
                    return Result.Fail("Author not found!");
                }

                _libraryDbContext.Remove(author);
                await _libraryDbContext.SaveChangesAsync();

                return Result.Ok();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failure on delete an author");
                return Result.Fail(error);
            }
        }
    }
}
