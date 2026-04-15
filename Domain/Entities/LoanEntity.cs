namespace Domain.Entities;

public class LoanEntity
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public virtual BookEntity? Book { get; set; } = null!;

}