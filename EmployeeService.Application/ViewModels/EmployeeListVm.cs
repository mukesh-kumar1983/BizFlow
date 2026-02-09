using EmployeeService.Application.DTOs;
using System.Collections.Generic;

namespace EmployeeService.Application.ViewModels
{
    /// <summary>
    /// ViewModel for listing employees with pagination metadata.
    /// </summary>
    public sealed class EmployeeListVm
    {
        /// <summary>
        /// List of employee DTOs for the current page.
        /// </summary>
        public IReadOnlyList<EmployeeDto> Employees { get; init; } = [];

        /// <summary>
        /// Current page number (1-based).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Total number of employees across all pages.
        /// </summary>
        public int TotalCount { get; set; } = 0;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EmployeeListVm() { }

        /// <summary>
        /// Convenience constructor from PagedResult<EmployeeDto>.
        /// </summary>
        public EmployeeListVm(Domain.Common.PagedResult<EmployeeDto> pagedResult)
        {
            Employees = pagedResult.Items;
            TotalCount = pagedResult.TotalCount;
            PageNumber = pagedResult.PageNumber;
            PageSize = pagedResult.PageSize;
        }
    }
}
