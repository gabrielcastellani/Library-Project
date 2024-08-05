using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Authors.Aggregates;

namespace Library.Api.Domain.Authors.Services.Interfaces
{
    public interface IAuthorsService
    {
        Task<Result<Author[]>> GetAll();
        Task<Result<Author>> Create(Author author);
        Task<Result> Update(Author author);
        Task<Result> Delete(Guid authorId);
    }
}
