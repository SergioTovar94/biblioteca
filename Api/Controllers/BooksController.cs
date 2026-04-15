using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Books;
using Api.DTOs;
using Shared;
using Api.DTOs.Books.Responses;
using Api.DTOs.Books.Requests;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly CreateBookUseCase _createHandler;
    private readonly GetBookUseCase _getHandler;
    private readonly ListBooksUseCase _listHandler;
    private readonly UpdateBookUseCase _updateHandler;
    private readonly DeleteBookUseCase _deleteHandler;

    public BooksController(
        CreateBookUseCase createHandler,
        GetBookUseCase getHandler,
        ListBooksUseCase listHandler,
        UpdateBookUseCase updateHandler,
        DeleteBookUseCase deleteHandler)
    {
        _createHandler = createHandler;
        _getHandler = getHandler;
        _listHandler = listHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> Create([FromBody] CreateBookRequestDto request, CancellationToken ct)
    {
        var command = new CreateBookCommand(
            request.Title, request.NumberOfPages, request.PublishedDate, request.Genre, request.AuthorId);
        var result = await _createHandler.Handle(command, ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        var response = new BookResponseDto
        {
            Id = result.Value,
            Title = request.Title,
            NumberOfPages = request.NumberOfPages,
            PublishedDate = request.PublishedDate,
            Genre = request.Genre,
            AuthorId = request.AuthorId
        };
        return CreatedAtAction(nameof(GetById), new { id = result.Value }, response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookResponseDto>> GetById(int id, CancellationToken ct)
    {
        var query = new GetBookQuery(id);
        var result = await _getHandler.Execute(query, ct);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        var book = result.Value;
        if (book is null)
            return NotFound();
        var response = new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            NumberOfPages = book.NumberOfPages,
            PublishedDate = book.PublishedDate,
            Genre = book.Genre,
            AuthorId = book.AuthorId,
            AuthorName = book.Author?.Name + " " + book.Author?.LastName
        };
        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetAll(CancellationToken ct)
    {
        var query = new ListBooksQuery();
        var result = await _listHandler.Handle(query, ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        var response = result.Value.Select(book => new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            NumberOfPages = book.NumberOfPages,
            PublishedDate = book.PublishedDate,
            Genre = book.Genre,
            AuthorId = book.AuthorId,
            AuthorName = book.Author?.Name + " " + book.Author?.LastName
        });
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookRequestDto request, CancellationToken ct)
    {
        var command = new UpdateBookCommand(
            id, request.Title, request.NumberOfPages, request.PublishedDate, request.Genre, request.AuthorId);
        var result = await _updateHandler.Handle(command, ct);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var command = new DeleteBookCommand(id);
        var result = await _deleteHandler.Handle(command, ct);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return NoContent();
    }
}