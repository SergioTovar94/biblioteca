using Application.Entities;
using Core.Domains.Authors;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    [HttpPost("Create")]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorEntity request,
        [FromServices] CreateAuthorDomain createAuthorDomain, CancellationToken ct)
    {
        var result = await createAuthorDomain.Create(request, ct);
        return Ok(result);
    }

    [HttpGet("List")]
    public async Task<IActionResult> ListAuthors([FromServices] AuthorListDomain authorListDomain, CancellationToken ct)
    {
        var result = await authorListDomain.GetAuthorsAsync(ct);
        return Ok(result);
    }
}