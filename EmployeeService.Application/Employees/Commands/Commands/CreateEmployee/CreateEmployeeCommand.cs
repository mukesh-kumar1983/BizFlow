using MediatR;

namespace EmployeeService.Application.Employees.Commands.CreateEmployee;

public sealed record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email
) : IRequest<Guid>;