using EmployeeService.Domain.Common;
using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Queries;
using EmployeeService.Domain.Repositories;
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

        public async Task<PagedResult<Employee>> GetPagedAsync(EmployeeQueryParameters query)
        {
            var employees = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
                employees = employees.Where(e =>
                    e.FirstName.Contains(query.Search) ||
                    e.LastName.Contains(query.Search));

            var total = await employees.CountAsync();

            var items = await employees
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedResult<Employee>(
                items,
                total,
                query.PageNumber,
                query.PageSize
            );
        }

        public Task<Employee?> GetByIdAsync(Guid id) =>
            _context.Employees.FindAsync(id).AsTask();

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var employee = await GetByIdAsync(id);
            if (employee == null) return;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
