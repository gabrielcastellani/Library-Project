using Library.Api.Domain.Authors.Aggregates;
using Library.Api.Domain.Authors.Services.Interfaces;
using Library.Contracts.Http.Requests.Authors;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _authorsService.GetAll();

            if (result.Success)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorRequest request)
        {
            var result = await _authorsService
                .Create(new Author
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                });

            if (result.Success)
            {
                return Created(Request.GetDisplayUrl(), result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthorAsync(Guid id, [FromBody] UpdateAuthorRequest request)
        {
            var result = await _authorsService
                .Update(new Author
                {
                    Id = id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate
                });

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorAsync(Guid id)
        {
            var result = await _authorsService.Delete(id);

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result.Error);
        }
    }
}
