using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Authors.Request;

public class CreateAuthorRequestDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
}