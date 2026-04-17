namespace Application.Abstractions.Books;

public interface IFileStorageService
{
    Task<string> SaveBookCoverAsync(Stream fileStream, string fileName, CancellationToken ct);
}