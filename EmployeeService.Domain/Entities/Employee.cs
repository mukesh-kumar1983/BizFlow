using EmployeeService.Domain.Common;
using System.ComponentModel.DataAnnotations;


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

        [Timestamp]
        public byte[] RowVersion { get; set; }   // 👈 must be byte[]

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
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.");

            FirstName = firstName.Trim();
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.");

            LastName = lastName.Trim();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            email = email.Trim().ToLowerInvariant();

            // Basic email format validation
            if (!System.Text.RegularExpressions.Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format.");
            }

            Email = email;
        }

    }
}
