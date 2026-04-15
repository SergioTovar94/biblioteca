
namespace Domain.Entities;

public class AuthorEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public ICollection<BookEntity>? Books { get; set; }
}