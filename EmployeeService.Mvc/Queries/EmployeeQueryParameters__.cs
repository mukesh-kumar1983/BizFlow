namespace EmployeeService.Application.Queries;

/// <summary>
/// Encapsulates paging, sorting and filtering parameters
/// for employee queries.
/// </summary>
public sealed class EmployeeQueryParameters__
{
    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    public string? Search { get; init; }

    public string SortBy { get; init; } = "FirstName";

    public string SortDirection { get; init; } = "asc";
}
