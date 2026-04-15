using Microsoft.AspNetCore.Mvc;
using Api.Dtos.Authors.Request;
using Api.Dtos.Authors.Responses;
using Application.UseCases.Authors;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{

    [HttpPost("Create")]
    public async Task<IActionResult> CreateAuthor(
        [FromBody] CreateAuthorRequestDto request,
        [FromServices] CreateAuthorUseCase useCase,
        CancellationToken ct)
    {
        var command = new CreateAuthorCommand(request.Name, request.LastName);
        var result = await useCase.Execute(command, ct);
        if (result.IsSuccess)
        {
            var response = new AuthorResponseDto
            {
                Id = result.Value,
                Name = request.Name,
                LastName = request.LastName
            };
            return CreatedAtAction(nameof(CreateAuthor), new { id = result.Value }, response);
        }
        else
        {
            return BadRequest(new { error = result.Error });
        }
    }

    [HttpGet("List")]
    public async Task<IActionResult> ListAuthors([FromServices] AuthorListUseCase authorListDomain, CancellationToken ct)
    {
        var result = await authorListDomain.GetAuthorsAsync(ct);
        return Ok(result);
    }
}