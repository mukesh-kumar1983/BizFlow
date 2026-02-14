using EmployeeService.Domain.Repositories;
using MediatR;
using System.Data;

namespace EmployeeService.Application.Employees.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandHandler
    : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository _repository;

    public UpdateEmployeeCommandHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetByIdAsync(request.Id);

        if (employee is null)
            throw new KeyNotFoundException("Employee not found.");

        // Update domain values with request data
        employee.SetFirstName(request.FirstName);
        employee.SetLastName(request.LastName);
        employee.SetEmail(request.Email);

        // Convert client-provided RowVersion (base64 string) to byte[]
        employee.RowVersion = Convert.FromBase64String(request.RowVersion);

        await _repository.UpdateAsync(employee);

        return Unit.Value;
    }

}
