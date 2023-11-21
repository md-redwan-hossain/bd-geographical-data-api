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
    public async Task<IActionResult> GetById(
        [BindRequired] [FromRoute(Name = "sub_district_id")]
        int id,
        [FromQuery(Name = "add_division")] bool addDivision = false,
        [FromQuery(Name = "add_district")] bool addDistrict = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _subDistrictService.FindOneById(id, addDistrict, addDivision);

        if (result is null)
            return NotFound("division not found");

        return Ok(result.ToDto(addDistrict, addDivision));
    }

    [HttpGet("{sub_district_name}")]
    public async Task<IActionResult> GetByEnglishName(
        [BindRequired] [FromRoute(Name = "sub_district_name")]
        string subDistrictName,
        [BindRequired] [FromQuery(Name = "district_name")]
        string districtName,
        [BindRequired] [FromQuery(Name = "division_name")]
        string divisionName,
        [FromQuery(Name = "add_division")] bool addDivision = false,
        [FromQuery(Name = "add_district")] bool addDistrict = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _subDistrictService.FindOneByEnglishName
            (subDistrictName, districtName, divisionName, addDistrict, addDivision);

        if (result is null)
            return NotFound("sub district not found");

        return Ok(result.ToDto(addDistrict, addDivision));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        ApiResponseSortOrder sortOrder,
        [FromQuery] ApiPagination apiPagination,
        [FromQuery(Name = "add_division")] bool addDivision = false,
        [FromQuery(Name = "add_district")] bool addDistrict = false
    )
    {
        var result = await _subDistrictService.FindAll(
            apiPagination.Page, apiPagination.Limit, sortOrder, addDistrict, addDivision);
        return Ok(result.Select(x => x.ToDto(true, true)));
    }
}