using EmployeeService.Domain.Common;

namespace EmployeeService.Domain.Entities
{
    /// <summary>
    /// Represents an employee in the system.
    /// This is a domain entity with encapsulated state.
    /// </summary>
    public sealed class Employee : Entity<Guid>
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        /// <summary>
        /// Computed full name (not stored in DB).
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        // Required by EF Core
        private Employee() { }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        public Employee(Guid id, string firstName, string lastName, string email)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            SetName(firstName, lastName);
            SetEmail(email);
        }

        /// <summary>
        /// Updates employee name.
        /// </summary>
        public void SetName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required");

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }

        /// <summary>
        /// Updates employee email.
        /// </summary>
        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            Email = email.Trim().ToLowerInvariant();
        }
    }
}
