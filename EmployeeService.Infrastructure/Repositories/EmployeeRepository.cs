using EmployeeService.Domain.Repositories;

using EmployeeService.Domain.Common;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Queries;

using EmployeeService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BizFlowDbContext _context;

        public EmployeeRepository(BizFlowDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Employee>> GetPagedAsync(EmployeeQueryParameters parameters)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(e =>
                    e.FirstName.Contains(parameters.Search) ||
                    e.LastName.Contains(parameters.Search) ||
                    e.Email.Contains(parameters.Search));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<Employee>(
                items,
                totalCount,
                parameters.PageNumber,
                parameters.PageSize);
        }


        public async Task AddAsync(Employee employee)
        {
            
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

    }
}
