using BdGeographicalData.Shared;
using BdGeographicalData.Api.SubDistrict.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BdGeographicalData.Api.SubDistrict;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class SubDistrictController : ControllerBase
{
    private readonly ISubDistrictService _subDistrictService;

    public SubDistrictController(ISubDistrictService subDistrictService) =>
        _subDistrictService = subDistrictService;


    [HttpGet("{sub_district_id:int}")]
    public async Task<IActionResult> GetById([BindRequired] [FromRoute(Name = "sub_district_id")] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _subDistrictService.FindOneById(id);

        if (result is null) return NotFound("division not found");

        return Ok(result.ToDto(true, true));
    }

    [HttpGet("{sub_district_name}")]
    public async Task<IActionResult> GetByEnglishName(
        [BindRequired] [FromRoute(Name = "sub_district_name")]
        string subDistrictName,
        [BindRequired] [FromQuery(Name = "district_name")]
        string districtName,
        [BindRequired] [FromQuery(Name = "division_name")]
        string divisionName)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _subDistrictService.FindOneByEnglishName(subDistrictName, districtName, divisionName);

        if (result is null) return NotFound("sub district not found");

        return Ok(result.ToDto(true, true));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        ApiResponseSortOrder sortOrder,
        [FromQuery(Name = "page")] ushort page = 1,
        [FromQuery(Name = "limit")] ushort limit = 1)
    {
        var result = await _subDistrictService.FindAll(page, limit, sortOrder);
        return Ok(result.Select(x => x.ToDto(true, true)));
    }
}