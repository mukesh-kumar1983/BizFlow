using EmployeeService.Application.DTOs;
using EmployeeService.Domain.Queries;
using EmployeeService.Domain.Common;

namespace EmployeeService.Application.Services
{
    public interface IEmployeeAppService
    {
        Task<PagedResult<EmployeeDto>> GetAsync(EmployeeQueryParameters query);
        Task<EmployeeDto?> GetByIdAsync(Guid id);
        Task<EmployeeDto> CreateAsync(EmployeeDto dto);
        Task UpdateAsync(Guid id, EmployeeDto dto);
        Task DeleteAsync(Guid id);
    }
}
