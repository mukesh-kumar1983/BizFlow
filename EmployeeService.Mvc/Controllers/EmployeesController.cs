using EmployeeService.Mvc.Services;
using EmployeeService.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Mvc.Controllers;

/// <summary>
/// MVC controller for Employee UI.
/// </summary>
public sealed class EmployeesController : Controller
{
    private readonly IEmployeeApiClient _api;

    public EmployeesController(IEmployeeApiClient api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index(
    int page = 1,
    int pageSize = 10,
    string? search = null,
    string sortBy = "FirstName",
    string sortDirection = "asc")
    {
        var apiVm = await _api.GetAsync(page, pageSize, search, sortBy, sortDirection);

        var vm = new EmployeeService.Mvc.ViewModels.EmployeeListVm
        {
            Employees = apiVm.Employees.Select(e => new EmployeeVm
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            }).ToList(),

            PageNumber = apiVm.PageNumber,
            PageSize = apiVm.PageSize,
            TotalCount = apiVm.TotalCount
        };

        return View(vm);
    }

}
