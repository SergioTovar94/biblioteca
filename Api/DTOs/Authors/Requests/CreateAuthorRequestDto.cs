using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Authors.Request;

public record CreateAuthorRequestDto
{

    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; init; } = string.Empty;

    public DateTime? BirthDate { get; init; }

    [MaxLength(100)]
    public string? Country { get; init; }

    public string? Biography { get; init; }
}