namespace EmployeeService.Domain.Queries
{
    /// <summary>
    /// Encapsulates query parameters for retrieving employees
    /// including paging, searching, and sorting.
    /// </summary>
    public sealed class EmployeeQueryParameters
    {
        // --------------------
        // Paging
        // --------------------

        /// <summary>
        /// Page number (1-based).
        /// </summary>
        public int PageNumber { get; init; } = 1;

        /// <summary>
        /// Number of records per page.
        /// </summary>
        public int PageSize { get; init; } = 20;

        // --------------------
        // Searching
        // --------------------

        /// <summary>
        /// Free-text search (FirstName, LastName, Email).
        /// </summary>
        public string? Search { get; init; }

        // --------------------
        // Sorting
        // --------------------

        /// <summary>
        /// Property name to sort by.
        /// </summary>
        public string SortBy { get; init; } = "FirstName";

        /// <summary>
        /// Sort direction: asc or desc.
        /// </summary>
        public string SortDirection { get; init; } = "asc";
    }
}
