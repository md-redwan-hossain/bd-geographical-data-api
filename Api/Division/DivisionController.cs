using BdGeographicalData.Api.Division.Entity;

namespace BdGeographicalData.Api.Division;

using Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class DivisionController : ControllerBase
{
    private readonly IDivisionService _divisionService;

    public DivisionController(IDivisionService divisionService) =>
        _divisionService = divisionService;


    [HttpGet("{division_id:int}")]
    public async Task<IActionResult> GetById([BindRequired] [FromRoute(Name = "division_id")] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _divisionService.FindOneById(id);

        if (result is null)
            return NotFound("division not found");

        return Ok(result.ToDto(true, true));
    }

    [HttpGet("{division_name}")]
    public async Task<IActionResult> GetByEnglishName(
        [BindRequired] [FromRoute(Name = "division_name")]
        string divisionName)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _divisionService.FindOneByEnglishName(divisionName);

        if (result is null)
            return NotFound("division not found");

        return Ok(result.ToDto(true, true));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        ApiResponseSortOrder sortOrder,
        [FromQuery(Name = "page")] ushort page = 1,
        [FromQuery(Name = "limit")] ushort limit = 1)
    {
        var result = await _divisionService.FindAll(page, limit, sortOrder);
        return Ok(result.Select(x => x.ToDto(true, true)));
    }
}