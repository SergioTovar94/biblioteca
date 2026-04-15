using Microsoft.AspNetCore.Mvc;
using Api.Dtos.Authors.Request;
using Api.Dtos.Authors.Responses;
using Application.UseCases.Authors;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{

    private readonly CreateAuthorUseCase _createAuthor;
    private readonly ListAuthorsUseCase _listAuthors;

    public AuthorController(
        CreateAuthorUseCase createAuthor,
        ListAuthorsUseCase listAuthors
        )
    {
        _createAuthor = createAuthor;
        _listAuthors = listAuthors;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateAuthor(
        [FromBody] CreateAuthorRequestDto request,
        CancellationToken ct)
    {
        var command = new CreateAuthorCommand(
            request.Name, request.LastName, request.BirthDate, request.Country, request.Biography
            );
        var result = await _createAuthor.Execute(command, ct);
        if (result.IsSuccess)
        {
            var response = new AuthorResponseDto
            {
                Id = result.Value,
                Name = request.Name,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Country = request.Country,
                Biography = request.Biography
            };
            return CreatedAtAction(nameof(CreateAuthor), new { id = result.Value }, response);
        }
        else
        {
            return BadRequest(new { error = result.Error });
        }
    }

    [HttpGet("List")]
    public async Task<ActionResult<PagedAuthorResponseDto>> GetAll(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string sortBy = "Name",
    [FromQuery] bool sortDescending = false,
    CancellationToken ct = default)
    {
        // Validar valores (opcional)
        page = page < 1 ? 1 : page;
        pageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);

        var query = new ListAuthorsQuery(page, pageSize, sortBy, sortDescending);
        var result = await _listAuthors.Handle(query, ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        var response = new PagedAuthorResponseDto
        {
            Items = result.Value.Items.Select(author => new AuthorResponseDto
            {
                Id = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Country = author.Country,
                Biography = author.Biography
            }),
            TotalCount = result.Value.TotalCount,
            Page = result.Value.Page,
            PageSize = result.Value.PageSize,
            TotalPages = result.Value.TotalPages
        };
        return Ok(response);
    }
}