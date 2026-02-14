using MediatR;

namespace EmployeeService.Application.Employees.Commands.DeleteEmployee;

public sealed record DeleteEmployeeCommand(Guid Id) : IRequest;
