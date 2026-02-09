using EmployeeService.Application.DTOs;
using EmployeeService.Application.Validators;
using FluentValidation.TestHelper;

public sealed class EmployeeDtoValidatorTests
{
    private readonly EmployeeDtoValidator _validator = new();

    [Fact]
    public void Should_fail_when_email_is_invalid()
    {
        var dto = new EmployeeDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "invalid"
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
}
