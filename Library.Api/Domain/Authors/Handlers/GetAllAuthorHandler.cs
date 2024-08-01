using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Aggregates;
using Library.Api.Domain.Authors.Commands;
using Library.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Domain.Authors.Handlers
{
    internal class GetAllAuthorHandler : IRequestHandler<GetAllAuthor, Result<Author[]>>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<GetAllAuthorHandler> _logger;

        public GetAllAuthorHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<GetAllAuthorHandler>();
        }

        public async Task<Result<Author[]>> Handle(GetAllAuthor request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = await _libraryDbContext
                    .Set<Database.Entities.Authors>()
                    .Select(author => new Author(author))
                    .ToArrayAsync(cancellationToken);

                return Result<Author[]>.Ok(authors);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failure on get all authors");
                return Result<Author[]>.Fail(error);
            }
        }
    }
}
