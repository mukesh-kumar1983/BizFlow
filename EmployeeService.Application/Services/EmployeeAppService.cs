using EmployeeService.Application.DTOs;
using EmployeeService.Domain.Common;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Queries;
using EmployeeService.Domain.Repositories;

namespace EmployeeService.Application.Services
{
    /// <summary>
    /// Application service responsible for employee-related use cases.
    /// Handles DTO ↔ Entity mapping and business rules.
    /// </summary>
    public class EmployeeAppService : IEmployeeAppService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeAppService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Returns a paged list of employees based on query parameters.
        /// </summary>
        public async Task<PagedResult<EmployeeDto>> GetAsync(EmployeeQueryParameters query)
        {
            var result = await _repository.GetPagedAsync(query);

            return new PagedResult<EmployeeDto>(
                result.Items.Select(MapToDto).ToList(),
                result.TotalCount,
                query.PageNumber,
                query.PageSize
            );
        }

        /// <summary>
        /// Returns a single employee by Id.
        /// </summary>
        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        public async Task<EmployeeDto> CreateAsync(EmployeeDto dto)
        {
            var entity = MapToEntity(dto);

            await _repository.AddAsync(entity);

            return MapToDto(entity);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        public async Task UpdateAsync(Guid id, EmployeeDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Employee not found");

            entity.SetName(dto.FirstName, dto.LastName);
            entity.SetEmail(dto.Email);

            await _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// Deletes an employee by Id.
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        // --------------------
        // Mapping helpers
        // --------------------

        private static EmployeeDto MapToDto(Employee entity) => new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email
        };

        /// <summary>
        /// Maps DTO to new Employee entity (used ONLY for Create).
        /// </summary>
        private static Employee MapToEntity(EmployeeDto dto)
        {
            return new Employee(
                dto.Id,
                dto.FirstName,
                dto.LastName,
                dto.Email
            );
        }

    }
}
