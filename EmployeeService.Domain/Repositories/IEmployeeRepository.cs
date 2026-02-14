using EmployeeService.Domain.Common;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Queries;

namespace EmployeeService.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id);
    Task<PagedResult<Employee>> GetPagedAsync(EmployeeQueryParameters parameters);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Employee employee);
}
