namespace Application.UseCases.Authors;

public record UpdateAuthorCommand(
    int Id,
    string Name,
    string LastName,
    DateTime? BirthDate,
    string? Country,
    string? Biography
    );
