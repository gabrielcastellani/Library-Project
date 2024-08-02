using Library.Api.Domain.Books.Commands;
using Library.Contracts.Http.Requests.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/books")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator
                .Send(new GetAllBook());

            if (result.Success)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookRequest request)
        {
            var result = await _mediator.Send(
                new CreateBook(
                    Name: request.Name,
                    Description: request.Description,
                    ReleaseDate: request.ReleaseDate,
                    AuthorId: request.AuthorId));

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] UpdateBookRequest request)
        {
            var result = await _mediator.Send(
                new UpdateBook(
                    Id: id,
                    Name: request.Name,
                    Description: request.Description,
                    ReleaseDate: request.ReleaseDate,
                    AuthorId: request.AuthorId));

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            var result = await _mediator.Send(new DeleteBook(id));

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }
    }
}
