using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Repositories;
using MediatR;

namespace EmployeeService.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IEmployeeRepository _repository;

        public CreateEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Email
            );

            await _repository.AddAsync(employee); // Assuming your repository handles EF Core Save

            return employee.Id;
        }
    }
}
