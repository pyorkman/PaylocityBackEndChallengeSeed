using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Api.Services;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private static List<Employee> employees = new List<Employee>();

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> GetEmployee(int id)
    {

        var employee = employees.FirstOrDefault(x => x.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAllEmployees()
    {
        var employees = DataContext.Data;

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Create new employee")]
    [HttpPost]
    public IActionResult CreateEmployee(GetEmployeeDto data)
    {
        var _employee = new Employee()
        {
            Id = data.Id,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Salary = data.Salary,
            DateOfBirth = data.DateOfBirth,
        };

        if(ModelState.IsValid)
        {
            employees.Add(_employee);
            return CreatedAtAction("GetEmployee", new { _employee.Id }, data);
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [SwaggerOperation(Summary = "Get paycheck for employee")]
    [HttpGet("{employeeId}/paycheck/get")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> GetPaycheck(int employeeId)
    {
        EmployeeService employeeService = new EmployeeService();    
        var paycheck = employeeService.GetPaycheck(employeeId);

        if (paycheck == null)
        {
            return StatusCode((int)HttpStatusCode.NotFound, new ApiResponse<GetPaycheckDto>
            {
                Success = false,
                Error = $"Employee with id {employeeId} not found"
            });
        }

        var result = new ApiResponse<GetPaycheckDto>
        {
            Data = paycheck,
            Success = true
        };

        return StatusCode((int)HttpStatusCode.OK, result);
    }
}
