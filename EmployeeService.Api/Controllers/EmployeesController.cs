using EmployeeService.Application.Services;
using EmployeeService.Domain.Queries;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeAppService _service;

    public EmployeesController(IEmployeeAppService service)
    {
        _service = service;
    }

    //[HttpGet]
    //public async Task<IActionResult> Get(
    //    int page = 1,
    //    int pageSize = 10,
    //    string? search = null,
    //    string sortBy = "FirstName",
    //    string sortDirection = "asc")
    //{
    //    var result = await _service.GetAsync(
    //        page, pageSize, search, sortBy, sortDirection);

    //    return Ok(result);
    //}

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] EmployeeQueryParameters query)
    {
        var result = await _service.GetAsync(query);
        return Ok(result);
    }
}
