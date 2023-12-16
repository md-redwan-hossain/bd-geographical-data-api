using BdGeographicalData.Api.SubDistrict.Entity;
using BdGeographicalData.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BdGeographicalData.Api.SubDistrict;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class SubDistrictController(ISubDistrictService subDistrictService) : ControllerBase
{
    private const string AddDivisionQueryKey = "add_division";
    private const string AddDistrictQueryKey = "add_district";

    [HttpGet("{sub_district_id:int}")]
    public async Task<IActionResult> GetById(
        [BindRequired] [FromRoute(Name = "sub_district_id")]
        int id,
        [FromQuery(Name = AddDivisionQueryKey)]
        bool addDivision = false,
        [FromQuery(Name = AddDistrictQueryKey)]
        bool addDistrict = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await subDistrictService.FindOneById(id, addDistrict, addDivision);

        if (result is null)
            return NotFound("sub-district not found");

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
        [FromQuery(Name = AddDivisionQueryKey)]
        bool addDivision = false,
        [FromQuery(Name = AddDistrictQueryKey)]
        bool addDistrict = false
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await subDistrictService.FindOneByEnglishName
            (subDistrictName, districtName, divisionName, addDistrict, addDivision);

        if (result is null)
            return NotFound("sub-district not found");

        return Ok(result.ToDto(addDistrict, addDivision));
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] ApiPagination apiPagination,
        [FromQuery(Name = Constant.SortingQueryKey)]
        ApiResponseSortOrder sortOrder,
        [FromQuery(Name = AddDivisionQueryKey)]
        bool addDivision = false,
        [FromQuery(Name = AddDistrictQueryKey)]
        bool addDistrict = false
    )
    {
        var result = await subDistrictService.FindAll(apiPagination, sortOrder, addDistrict, addDivision);
        return Ok(result.Select(x => x.ToDto(addDistrict, addDivision)));
    }
}