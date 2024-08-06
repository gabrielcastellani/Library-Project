using Library.Api.Domain.Abstracts;
using MediatR;

namespace Library.Api.Domain.Authors.Commands
{
    internal record UpdateAuthorCommand(
        Guid Id,
        string FirstName,
        string LastName,
        DateTime BirthDate)
        : IRequest<Result>;
}
