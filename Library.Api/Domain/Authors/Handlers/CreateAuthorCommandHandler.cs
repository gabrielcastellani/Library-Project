using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Aggregates;
using Library.Api.Domain.Authors.Commands;
using Library.Database.Context;
using MediatR;

namespace Library.Api.Domain.Authors.Handlers
{
    internal class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<Author>>
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<CreateAuthorCommandHandler> _logger;

        public CreateAuthorCommandHandler(
            ILoggerFactory loggerFactory,
            LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
            _logger = loggerFactory.CreateLogger<CreateAuthorCommandHandler>();
        }

        public Task<Result<Author>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            Result<Author> result;

            try
            {
                var author = new Author(request);

                _libraryDbContext
                    .Set<Database.Entities.Authors>()
                    .Add(author.ToEntity());

                _libraryDbContext.SaveChangesAsync(cancellationToken);

                result = Result<Author>.Ok(author);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Failure on create a new author.");
                result = Result<Author>.Fail(error);
            }

            return Task.FromResult(result);
        }
    }
}
