using Application.Abstractions.Books;

namespace Infrastructure.FileStorage;

public class FileStorageService : IFileStorageService
{
    private readonly string _baseFolder;

    public FileStorageService()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        _baseFolder = Path.Combine(currentDirectory, "wwwroot", "uploads", "books");
    }

    public async Task<string> SaveBookCoverAsync(Stream fileStream, string fileName, CancellationToken ct)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
        if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            throw new ArgumentException("Formato de imagen no permitido. Use .jpg, .jpeg, .png o .webp.");

        if (!Directory.Exists(_baseFolder))
            Directory.CreateDirectory(_baseFolder);

        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(_baseFolder, uniqueFileName);

        using (var fileStreamOutput = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(fileStreamOutput, ct);
        }

        return $"/uploads/books/{uniqueFileName}";
    }
}