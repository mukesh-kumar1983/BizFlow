using EmployeeService.Application.ViewModels;

namespace EmployeeService.Mvc.Services;

/// <summary>
/// Contract for communicating with EmployeeService API.
/// This abstraction allows the MVC layer to remain
/// decoupled from HttpClient and API details.
/// </summary>
public interface IEmployeeApiClient
{
    /// <summary>
    /// Retrieves a paginated list of employees from the API.
    /// </summary>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of records per page</param>
    /// <param name="search">Optional search term</param>
    /// <param name="sortBy">Field name to sort by</param>
    /// <param name="sortDirection">asc / desc</param>
    /// <returns>Paged list of employees</returns>
    Task<EmployeeListVm> GetAsync(
        int page,
        int pageSize,
        string? search = null,
        string sortBy = "FirstName",
        string sortDirection = "asc");

    /// <summary>
    /// Retrieves a single employee by Id.
    /// </summary>
    Task<EmployeeDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    Task CreateAsync(EmployeeDto dto);

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    Task UpdateAsync(Guid id, EmployeeDto dto);

    /// <summary>
    /// Deletes an employee by Id.
    /// </summary>
    Task DeleteAsync(Guid id);
}
