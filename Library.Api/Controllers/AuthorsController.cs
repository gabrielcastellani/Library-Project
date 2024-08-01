using Library.Api.Domain.Authors.Commands;
using Library.Contracts.Http.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllAuthor());

            if (result.Success)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorRequest request)
        {
            var result = await _mediator.Send(
                new CreateAuthor(
                    FirstName: request.FirstName,
                    LastName: request.LastName,
                    BirthDate: request.BirthDate));

            if (result.Success)
            {
                return Created(Request.GetDisplayUrl(), result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthorAsync(Guid id, [FromBody] UpdateAuthorRequest request)
        {
            var result = await _mediator.Send(
                new UpdateAuthor(
                    Id: id,
                    FirstName: request.FirstName,
                    LastName: request.LastName,
                    BirthDate: request.BirthDate));

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorAsync(Guid id)
        {
            var result = await _mediator.Send(new DeleteAuthor(id));

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }
    }
}
