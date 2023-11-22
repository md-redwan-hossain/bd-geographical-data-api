using BdGeographicalData.Api.District.Entity;
using BdGeographicalData.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BdGeographicalData.Api.District;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class DistrictController : ControllerBase
{
    private readonly IDistrictService _districtService;

    public DistrictController(IDistrictService districtService) =>
        _districtService = districtService;


    [HttpGet("{district_id:int}")]
    public async Task<IActionResult> GetById(
        [BindRequired] [FromRoute(Name = "district_id")]
        int id,
        [FromQuery(Name = "add_division")] bool addDivision = false,
        [FromQuery(Name = "add_sub_districts")]
        bool addSubDistricts = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _districtService.FindOneById(id, addDivision, addSubDistricts);

        if (result is null)
            return NotFound("district not found");

        return Ok(result.ToDto(addDivision, addSubDistricts));
    }

    [HttpGet("{district_name}")]
    public async Task<IActionResult> GetByEnglishName(
        [BindRequired] [FromRoute(Name = "district_name")]
        string districtName,
        [BindRequired] [FromQuery(Name = "division_name")]
        string divisionName,
        [FromQuery(Name = "add_division")] bool addDivision = false,
        [FromQuery(Name = "add_sub_districts")]
        bool addSubDistricts = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result =
            await _districtService.FindOneByEnglishName(districtName, divisionName, addDivision, addSubDistricts);

        if (result is null)
            return NotFound("sub district not found");

        return Ok(result.ToDto(addDivision, addSubDistricts));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        ApiResponseSortOrder sortOrder,
        [FromQuery] ApiPagination apiPagination,
        [FromQuery(Name = "add_division")] bool addDivision = false,
        [FromQuery(Name = "add_sub_districts")]
        bool addSubDistricts = false)
    {
        var result = await _districtService.FindAll(
            apiPagination, sortOrder, addDivision, addSubDistricts);

        return Ok(result.Select(x => x.ToDto(addDivision, addSubDistricts)));
    }
}