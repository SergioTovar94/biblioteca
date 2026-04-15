using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Authors.Request;

public class UpdateAuthorRequestDto
{

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }

    public string? Biography { get; set; }
}