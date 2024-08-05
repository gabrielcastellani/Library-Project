using Library.Api.Domain.Books.Aggregates;
using Library.Api.Domain.Books.Services.Interfaces;
using Library.Contracts.Http.Requests.Books;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _booksService.GetAll();

            if (result.Success)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookRequest request)
        {
            var result = await _booksService
                .Create(new Book
                {
                    Name = request.Name,
                    Description = request.Description,
                    ReleaseDate = request.ReleaseDate,
                    AuthorId = request.AuthorId,
                });

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] UpdateBookRequest request)
        {
            var result = await _booksService
                .Update(new Book
                {
                    Id = id,
                    Name = request.Name,
                    Description = request.Description,
                    ReleaseDate = request.ReleaseDate,
                    AuthorId = request.AuthorId,
                });

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            var result = await _booksService.Delete(id);

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }
    }
}
