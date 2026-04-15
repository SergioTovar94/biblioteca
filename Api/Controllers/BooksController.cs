using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Books;
using Api.DTOs;
using Shared;
using Api.DTOs.Books.Responses;
using Api.DTOs.Books.Requests;
using Application.Abstractions.Books;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly CreateBookUseCase _createBookUseCase;
    private readonly IFileStorageService _fileStorageService;
    private readonly GetBookUseCase _getBookUseCase;
    private readonly ListBooksUseCase _listBooksUseCase;
    private readonly UpdateBookUseCase _updateBookUseCase;
    private readonly DeleteBookUseCase _deleteBookUseCase;

    public BooksController(
        CreateBookUseCase createBookUseCase,
        IFileStorageService fileStorageService,
        GetBookUseCase getBookUseCase,
        ListBooksUseCase listBooksUseCase,
        UpdateBookUseCase updateBookUseCase,
        DeleteBookUseCase deleteBookUseCase
        )
    {
        _createBookUseCase = createBookUseCase;
        _fileStorageService = fileStorageService;
        _getBookUseCase = getBookUseCase;
        _listBooksUseCase = listBooksUseCase;
        _updateBookUseCase = updateBookUseCase;
        _deleteBookUseCase = deleteBookUseCase;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<BookResponseDto>> Create(
        [FromForm] CreateBookMultipartRequestDto request,
        CancellationToken ct)
    {
        string coverImagePath;
        try
        {
            using var stream = request.CoverImage.OpenReadStream();
            coverImagePath = await _fileStorageService.SaveBookCoverAsync(stream, request.CoverImage.FileName, ct);
        }
        catch (ArgumentException ex)
        {

            return BadRequest(new { error = ex.Message });
        }
        var command = new CreateBookCommand(
            request.Title,
            request.NumberOfPages,
            request.PublishedDate,
            request.Genre,
            request.AuthorId,
            coverImagePath
        );
        var result = await _createBookUseCase.Handle(command, ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        var response = new BookResponseDto
        {
            Id = result.Value,
            Title = request.Title,
            NumberOfPages = request.NumberOfPages,
            PublishedDate = request.PublishedDate,
            Genre = request.Genre,
            AuthorId = request.AuthorId,
            CoverImageUrl = coverImagePath
        };
        return CreatedAtAction(nameof(GetById), new { id = result.Value }, response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookResponseDto>> GetById(int id, CancellationToken ct)
    {
        var query = new GetBookQuery(id);
        var result = await _getBookUseCase.Handle(query, ct);

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
        var result = await _listBooksUseCase.Handle(query, ct);

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
            CoverImageUrl = book.CoverImagePath,
            AuthorName = book.Author?.Name + " " + book.Author?.LastName
        });
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookRequestDto request, CancellationToken ct)
    {
        var command = new UpdateBookCommand(
            id, request.Title, request.NumberOfPages, request.PublishedDate, request.Genre, request.AuthorId);
        var result = await _updateBookUseCase.Handle(command, ct);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var command = new DeleteBookCommand(id);
        var result = await _deleteBookUseCase.Handle(command, ct);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return NoContent();
    }
}