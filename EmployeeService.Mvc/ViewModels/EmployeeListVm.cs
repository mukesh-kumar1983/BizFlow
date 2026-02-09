namespace EmployeeService.Mvc.ViewModels
{
    /// <summary>
    /// ViewModel for listing employees in the MVC view.
    /// </summary>
    public sealed class EmployeeListVm
    {
        /// <summary>
        /// The list of employees to display.
        /// </summary>
        public List<EmployeeVm> Employees { get; set; } = new List<EmployeeVm>();

        /// <summary>
        /// Current page number (optional for pagination).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Number of items per page (optional for pagination).
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Total number of employees across all pages.
        /// </summary>
        public int TotalCount { get; set; } = 0;
    }
}
