using System.Net.Http;
using System.Net.Http.Json;
using EmployeeService.Application.ViewModels;

namespace EmployeeService.Mvc.Services;

/// <summary>
/// HttpClient-based implementation of <see cref="IEmployeeApiClient"/>.
/// 
/// ✔ Uses Typed HttpClient
/// ✔ Uses BaseAddress from configuration
/// ✔ Uses relative URLs only (CRITICAL)
/// ✔ Handles HTTP failures explicitly
/// </summary>
public sealed class EmployeeApiClient : IEmployeeApiClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Constructor called by DI container.
    /// HttpClient is injected and preconfigured in Program.cs.
    /// </summary>
    public EmployeeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<EmployeeListVm> GetAsync(
        int page,
        int pageSize,
        string? search = null,
        string sortBy = "FirstName",
        string sortDirection = "asc")
    {
        // IMPORTANT:
        // No leading slash — BaseAddress already ends with /api/
        var query =
            $"api/employees?" +
            $"page={page}&" +
            $"pageSize={pageSize}&" +
            $"sortBy={sortBy}&" +
            $"sortDirection={sortDirection}";

        if (!string.IsNullOrWhiteSpace(search))
        {
            query += $"&search={Uri.EscapeDataString(search)}";
        }

        var response = await _httpClient.GetAsync(query);

        // Explicit error handling → easier debugging
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<EmployeeListVm>()
               ?? new EmployeeListVm();
    }

    /// <inheritdoc />
    public async Task<EmployeeDto?> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/employees/{id}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<EmployeeDto>();
    }

    /// <inheritdoc />
    public async Task CreateAsync(EmployeeDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/employees", dto);
        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, EmployeeDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/employees/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/employees/{id}");
        response.EnsureSuccessStatusCode();
    }
}
