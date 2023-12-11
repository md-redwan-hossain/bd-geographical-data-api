using BdGeographicalData.Api.Division.Entity;

namespace BdGeographicalData.Api.Division;

using Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class DivisionController(IDivisionService divisionService) : ControllerBase
{
    [HttpGet("{division_id:int}")]
    public async Task<IActionResult> GetById(
        [BindRequired] [FromRoute(Name = "division_id")]
        int id,
        [FromQuery(Name = "add_districts")] bool addDistricts = false,
        [FromQuery(Name = "add_sub_districts")]
        bool addSubDistricts = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await divisionService.FindOneById(id, addDistricts, addSubDistricts);

        if (result is null)
            return NotFound("division not found");

        return Ok(result.ToDto(addDistricts, addSubDistricts));
    }

    [HttpGet("{division_name}")]
    public async Task<IActionResult> GetByEnglishName(
        [BindRequired] [FromRoute(Name = "division_name")]
        string divisionName,
        [FromQuery(Name = "add_districts")] bool addDistricts = false,
        [FromQuery(Name = "add_sub_districts")]
        bool addSubDistricts = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await divisionService.FindOneByEnglishName(divisionName, addDistricts, addSubDistricts);

        if (result is null)
            return NotFound("division not found");

        return Ok(result.ToDto(addDistricts, addSubDistricts));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        ApiResponseSortOrder sortOrder,
        [FromQuery] ApiPagination apiPagination,
        [FromQuery(Name = "add_districts")] bool addDistricts = false,
        [FromQuery(Name = "add_sub_districts")]
        bool addSubDistricts = false
    )
    {
        var result = await divisionService.FindAll(
            apiPagination, sortOrder, addDistricts, addSubDistricts);

        return Ok(result.Select(x => x.ToDto(addDistricts, addSubDistricts)));
    }
}