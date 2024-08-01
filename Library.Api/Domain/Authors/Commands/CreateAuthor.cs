using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Aggregates;
using MediatR;

namespace Library.Api.Domain.Authors.Commands
{
    internal record CreateAuthor(string FirstName, string LastName, DateTime BirthDate)
        : IRequest<Result<Author>>;
}
