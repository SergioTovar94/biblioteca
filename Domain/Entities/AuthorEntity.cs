
namespace Domain.Entities;

public class AuthorEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Country { get; set; } = string.Empty;
    public string? Biography { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public ICollection<BookEntity>? Books { get; set; }
}