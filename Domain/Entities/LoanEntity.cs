namespace Domain.Entities;

public class LoanEntity
{
    public int id { get; set; }
    public int bookId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public LoanStatus Status { get; set; }

}