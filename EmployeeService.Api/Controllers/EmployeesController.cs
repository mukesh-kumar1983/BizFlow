using EmployeeService.Application.Employees.Commands.CreateEmployee;
using EmployeeService.Application.Employees.Commands.DeleteEmployee;
using EmployeeService.Application.Employees.Commands.UpdateEmployee;
using EmployeeService.Application.Employees.Queries.GetEmployees;
using EmployeeService.Application.Employees.Queries.GetEmployeeById;

using EmployeeService.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Api.Models;

namespace EmployeeService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ----------------------------
    // GET: api/employees
    // ----------------------------
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int draw,
        [FromQuery] int start,
        [FromQuery] int length)
    {
        var pageNumber = (start / length) + 1;

        var result = await _mediator.Send(
            new GetEmployeesQuery(pageNumber, length));

        return Ok(new
        {
            draw = draw,
            recordsTotal = result.TotalCount,
            recordsFiltered = result.TotalCount,
            data = result.Items
        });
    }

    // ----------------------------
    // GET: api/employees/{id}
    // ----------------------------
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));

        if (employee is null)
            return NotFound();

        return Ok(employee);
    }

    // ----------------------------
    // POST: api/employees
    // ----------------------------
   
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    // -----------------------
    // PUT (Update Employee)
    // -----------------------
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID in URL and body must match");

        await _mediator.Send(command);
        return NoContent();
    }


    // ----------------------------
    // DELETE: api/employees/{id}
    // ----------------------------
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));
        return NoContent();
    }
}
