namespace BizFlow.Web.Models
{
    // Create a matching PagedResult in MVC project
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string RowVersion { get; set; } = "";
        public string FullName => $"{FirstName} {LastName}";
    }

}
