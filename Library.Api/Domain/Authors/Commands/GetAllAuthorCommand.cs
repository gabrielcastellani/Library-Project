using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Aggregates;
using MediatR;

namespace Library.Api.Domain.Authors.Commands
{
    internal record GetAllAuthorCommand() : IRequest<Result<Author[]>>;
}
