using EmployeeService.Domain.Repositories;
using MediatR;

namespace EmployeeService.Application.Employees.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommandHandler
    : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IEmployeeRepository _repository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetByIdAsync(request.Id);

        if (employee is null)
            throw new KeyNotFoundException("Employee not found.");

        await _repository.DeleteAsync(employee);

        return Unit.Value; // ✅ REQUIRED
    }
}
