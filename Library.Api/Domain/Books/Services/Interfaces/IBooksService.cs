using Library.Api.Domain.Abstracts;
using Library.Api.Domain.Books.Aggregates;

namespace Library.Api.Domain.Books.Services.Interfaces
{
    public interface IBooksService
    {
        Task<Result<Book[]>> GetAll(Guid? authorId = null);
        Task<Result<Book>> Create(Book book);
        Task<Result> Update(Book book);
        Task<Result> Delete(Guid bookId);
    }
}
