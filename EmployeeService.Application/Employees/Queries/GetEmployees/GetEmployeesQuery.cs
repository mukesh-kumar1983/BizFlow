using MediatR;
using EmployeeService.Application.Common.Models;
using EmployeeService.Application.DTOs;

namespace EmployeeService.Application.Employees.Queries.GetEmployees;

public record GetEmployeesQuery(int PageNumber, int PageSize)
    : IRequest<PagedResult<EmployeeDto>>;
