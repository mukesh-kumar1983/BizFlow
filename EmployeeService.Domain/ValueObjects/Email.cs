using System.Text.RegularExpressions;

namespace EmployeeService.Domain.ValueObjects;

/// <summary>
/// Email value object (immutable).
/// </summary>
public sealed class Email : IEquatable<Email>
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    /// <summary>
    /// Email value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Required by EF Core.
    /// </summary>
    private Email() => Value = string.Empty;

    /// <summary>
    /// Creates a valid email.
    /// </summary>
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.");

        if (!EmailRegex.IsMatch(value))
            throw new ArgumentException("Invalid email format.");

        Value = value.Trim().ToLowerInvariant();
    }

    public override string ToString() => Value;

    // ----------------------------
    // Equality
    // ----------------------------

    public bool Equals(Email? other)
        => other is not null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as Email);

    public override int GetHashCode()
        => Value.GetHashCode();

    public static implicit operator string(Email email) => email.Value;
}
