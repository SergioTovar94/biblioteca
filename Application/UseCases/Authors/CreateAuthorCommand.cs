namespace Application.UseCases.Authors;

public record CreateAuthorCommand(
    string Name,
    string LastName,
    DateTime? BirthDate,
    string? Country,
    string? Biography
);
