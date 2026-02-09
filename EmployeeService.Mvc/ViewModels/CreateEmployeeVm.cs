using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Mvc.ViewModels;

public sealed class CreateEmployeeVm
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}
