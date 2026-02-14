using MediatR;

namespace EmployeeService.Application.Employees.Commands.UpdateEmployee;

public sealed record UpdateEmployeeCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string RowVersion
) : IRequest<Unit>;
