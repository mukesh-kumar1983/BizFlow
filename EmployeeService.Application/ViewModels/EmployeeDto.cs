using System;

namespace EmployeeService.Application.ViewModels
{
    /// <summary>
    /// Data Transfer Object for Employee.
    /// </summary>
    public sealed class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
    }
}
