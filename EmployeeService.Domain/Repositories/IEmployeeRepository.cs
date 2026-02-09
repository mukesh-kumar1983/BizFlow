using EmployeeService.Domain.Common;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Queries;

namespace EmployeeService.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<PagedResult<Employee>> GetPagedAsync(EmployeeQueryParameters query);
        Task<Employee?> GetByIdAsync(Guid id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
    }
}
