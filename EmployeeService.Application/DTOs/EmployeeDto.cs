namespace EmployeeService.Application.DTOs
{
    public sealed class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
    }
}
