namespace Domain.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int NumberOfPages { get; set; }
    public int AuthorId { get; set; }
    public virtual AuthorEntity? Author { get; set; }
}