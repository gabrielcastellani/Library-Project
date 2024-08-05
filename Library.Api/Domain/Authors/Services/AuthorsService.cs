using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Aggregates;
using Library.Api.Domain.Authors.Commands;
using Library.Api.Domain.Authors.Services.Interfaces;
using MediatR;

namespace Library.Api.Domain.Authors.Services
{
    internal sealed class AuthorsService : IAuthorsService
    {
        private readonly IMediator _mediator;

        public AuthorsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Result<Author[]>> GetAll()
        {
            return _mediator.Send(new GetAllAuthor());
        }

        public Task<Result<Author>> Create(Author author)
        {
            return _mediator.Send(
                new CreateAuthor(
                    FirstName: author.FirstName,
                    LastName: author.LastName,
                    BirthDate: author.BirthDate));
        }

        public Task<Result> Update(Author author)
        {
            return _mediator.Send(
                new UpdateAuthor(
                    Id: author.Id,
                    FirstName: author.FirstName,
                    LastName: author.LastName,
                    BirthDate: author.BirthDate));
        }

        public Task<Result> Delete(Guid authorId)
        {
            return _mediator.Send(new DeleteAuthor(authorId));
        }
    }
}
