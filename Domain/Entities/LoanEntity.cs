namespace Domain.Entities;

public class LoanEntity
{
    public int id;
    public int bookId;
    public string BorrowerName;
    public DateTime LoanDate;
    public DateTime DueDate;
    public DateTime ReturnDate;
    public LoanStatus Status;

}