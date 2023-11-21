using Api.Dtos.Dependent;
using Api.Models;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private static List<Dependent> dependents = new List<Dependent>();
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> GetDependents(int id)
    {
        var dependent = dependents.FirstOrDefault(x => x.Id == id);
        if (dependent == null)
        {
            return NotFound();
        }
        return Ok(dependent);
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAllDependents()
    {
        var dependents = DataContext.Data.Where(e => e.Dependents.Any()).SelectMany(e => e.Dependents);

        return Task.FromResult<ActionResult<ApiResponse<List<GetDependentDto>>>>
        (
            dependents == null ? NotFound() : Ok(dependents)
        );
    }
}
