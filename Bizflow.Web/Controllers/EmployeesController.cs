using BizFlow.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BizFlow.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            // Use named client configured in Program.cs
            _httpClient = httpClientFactory.CreateClient("EmployeeApi");
        }

        // GET: Employees/Index
        public IActionResult Index()
        {
            // The Index view just renders the table; data is fetched via AJAX
            return View();
        }

        // POST: Employees/GetEmployees
        [HttpPost]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var form = Request.Form;

                // DataTables parameters
                var draw = form["draw"].FirstOrDefault();
                var start = Convert.ToInt32(form["start"].FirstOrDefault());
                var length = Convert.ToInt32(form["length"].FirstOrDefault());
                var searchValue = form["search[value]"].FirstOrDefault();

                var sortColumnIndex = form["order[0][column]"].FirstOrDefault();
                var sortColumn = form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();
                var sortDirection = form["order[0][dir]"].FirstOrDefault();

                // Build request object
                var dataTableRequest = new
                {
                    Draw = draw,
                    Start = start,
                    Length = length,
                    SearchValue = searchValue,
                    SortColumn = sortColumn,
                    SortDirection = sortDirection
                };

                // Call API
                //var response = await _httpClient.PostAsJsonAsync("api/employees/getpaged", dataTableRequest);

                var response = await _httpClient.GetAsync(
                    $"employees?draw={draw}&start={start}&length={length}");

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        draw,
                        recordsTotal = 0,
                        recordsFiltered = 0,
                        data = new List<EmployeeViewModel>(),
                        error = "Unable to fetch data from API"
                    });
                }

                var result = await response.Content.ReadFromJsonAsync<object>();

                return Json(result);
            }
            catch (Exception ex)
            {
                // Handle exception gracefully
                return Json(new
                {
                    draw = 0,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<EmployeeViewModel>(),
                    error = ex.Message
                });
            }
        }
    }
}
