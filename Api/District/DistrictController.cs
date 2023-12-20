using BdGeographicalData.Api.District.Entity;
using BdGeographicalData.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BdGeographicalData.Api.District;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class DistrictController(IDistrictService districtService) : ControllerBase
{
    private const string AddDivisionQueryKey = "add_division";
    private const string AddSubDistrictsQueryKey = "add_sub_districts";

    [HttpGet("{district_id:int}")]
    public async Task<IActionResult> GetById(
        [BindRequired] [FromRoute(Name = "district_id")]
        int id,
        [FromQuery(Name = AddDivisionQueryKey)]
        bool addDivision = false,
        [FromQuery(Name = AddSubDistrictsQueryKey)]
        bool addSubDistricts = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await districtService.FindOneById(id, addDivision, addSubDistricts);

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
        [FromQuery(Name = AddDivisionQueryKey)]
        bool addDivision = false,
        [FromQuery(Name = AddSubDistrictsQueryKey)]
        bool addSubDistricts = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result =
            await districtService.FindOneByEnglishName(districtName, divisionName, addDivision, addSubDistricts);

        if (result is null)
            return NotFound("district not found");

        return Ok(result.ToDto(addDivision, addSubDistricts));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] ApiPagination apiPagination,
        [FromQuery(Name = Constants.SortingQueryKey)]
        ApiResponseSortOrder sortOrder,
        [FromQuery(Name = AddDivisionQueryKey)]
        bool addDivision = false,
        [FromQuery(Name = AddSubDistrictsQueryKey)]
        bool addSubDistricts = false
    )
    {
        var result = await districtService.FindAll(
            apiPagination, sortOrder, addDivision, addSubDistricts);

        return Ok(result.Select(x => x.ToDto(addDivision, addSubDistricts)));
    }
}