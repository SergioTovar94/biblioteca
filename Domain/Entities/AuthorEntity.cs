
namespace Domain.Entities;

public class AuthorEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string? Country { get; set; }
    public string? Biography { get; set; }
    public bool IsDeleted { get; set; } = false;
    public ICollection<BookEntity>? Books { get; set; }
}