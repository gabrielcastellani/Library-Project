using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Commands;
using Library.Database.Context;
using MediatR;

namespace Library.Api.Domain.Authors.Handlers
{
    internal class UpdateAuthorHandler : IRequestHandler<UpdateAuthor, Result>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<UpdateAuthorHandler> _logger;

        public UpdateAuthorHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<UpdateAuthorHandler>();
        }

        public async Task<Result> Handle(UpdateAuthor request, CancellationToken cancellationToken)
        {
            try
            {
                var author = _libraryDbContext
                    .Set<Database.Entities.Authors>()
                    .FirstOrDefault(entity => entity.Id == request.Id);

                if (author == null)
                {
                    return Result.Fail("Author not found!");
                }

                author.FirstName = request.FirstName;
                author.LastName = request.LastName;
                author.BirthDate = request.BirthDate;

                await _libraryDbContext.SaveChangesAsync(cancellationToken);

                return Result.Ok();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failure on update an author");
                return Result.Fail(error);
            }
        }
    }
}
