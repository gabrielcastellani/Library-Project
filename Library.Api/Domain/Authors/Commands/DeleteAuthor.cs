using Library.Api.Domain.Abstracts;
using MediatR;

namespace Library.Api.Domain.Authors.Commands
{
    internal record DeleteAuthor(Guid Id) : IRequest<Result>;
}
