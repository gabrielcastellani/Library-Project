using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Aggregates;
using Library.Api.Domain.Authors.Commands;
using Library.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Domain.Authors.Handlers
{
    internal class GetAllAuthorCommandHandler : IRequestHandler<GetAllAuthorCommand, Result<Author[]>>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<GetAllAuthorCommandHandler> _logger;

        public GetAllAuthorCommandHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<GetAllAuthorCommandHandler>();
        }

        public async Task<Result<Author[]>> Handle(GetAllAuthorCommand request, CancellationToken cancellationToken)
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
