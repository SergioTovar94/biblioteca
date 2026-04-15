using System.ComponentModel.DataAnnotations;
namespace Api.DTOs.Books.Requests;

public record CreateBookMultipartRequestDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; init; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "NumberOfPages debe ser mayor que 0")]
    public int NumberOfPages { get; init; }
    public DateTime PublishedDate { get; init; }

    [MaxLength(50)]
    public string Genre { get; init; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int AuthorId { get; init; }

    public IFormFile? CoverImage { get; set; }
}