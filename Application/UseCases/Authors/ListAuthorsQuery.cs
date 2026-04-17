namespace Application.UseCases.Authors;

public record ListAuthorsQuery(
    int Page = 1,
    int PageSize = 10,
    string SortBy = "Name",
    bool SortDescending = false
);