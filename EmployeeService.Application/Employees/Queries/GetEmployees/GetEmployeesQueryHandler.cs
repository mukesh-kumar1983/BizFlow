using EmployeeService.Application.Common.Interfaces;
using EmployeeService.Application.Common.Models;
using EmployeeService.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Application.Employees.Queries.GetEmployees;

public class GetEmployeesQueryHandler
    : IRequestHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<EmployeeDto>> Handle(
        GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Employees.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(e => e.FirstName)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<EmployeeDto>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}
