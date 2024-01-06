using BdGeographicalData.Domain.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BdGeographicalData.HttpApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class DivisionController(IDivisionService divisionService) : ControllerBase
{
    private const string AddDistrictsQueryKey = "add_districts";
    private const string AddSubDistrictsQueryKey = "add_sub_districts";

    [HttpGet("{division_id:int}")]
    public async Task<IActionResult> GetById(
        [BindRequired] [FromRoute(Name = "division_id")]
        int id,
        [FromQuery(Name = AddDistrictsQueryKey)]
        bool addDistricts = false,
        [FromQuery(Name = AddSubDistrictsQueryKey)]
        bool addSubDistricts = false
    )
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await divisionService.FindOneById(id, (addDistricts, addSubDistricts));

        if (result is null) return NotFound("division not found");

        return Ok(new
        {
            division = result.Item1,
            districts = result.Item2.Select(x => new { district = x.Item1, subDistricts = x.Item2 })
        });
    }

    // [HttpGet("{division_name}")]
    // public async Task<IActionResult> GetByEnglishName(
    //     [BindRequired] [FromRoute(Name = "division_name")]
    //     string divisionName,
    //     [FromQuery(Name = AddDistrictsQueryKey)]
    //     bool addDistricts = false,
    //     [FromQuery(Name = AddSubDistrictsQueryKey)]
    //     bool addSubDistricts = false
    // )
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest();
    //
    //     var result = await divisionService.FindOneByEnglishName(divisionName, addDistricts, addSubDistricts);
    //
    // if (result is null)
    //     return NotFound("division not found");
    //
    // return Ok(result.ToDto(addDistricts, addSubDistricts));

    // }


    // [HttpGet]
    // public async Task<IActionResult> GetAll(
    //     [FromQuery] ApiPagination apiPagination,
    //     [FromQuery(Name = Constants.SortingQueryKey)]
    //     ApiResponseSortOrder sortOrder,
    //     [FromQuery(Name = AddDistrictsQueryKey)]
    //     bool addDistricts = false,
    //     [FromQuery(Name = AddSubDistrictsQueryKey)]
    //     bool addSubDistricts = false
    // )
    // {
    //     var result = await divisionService.FindAll(
    //         apiPagination, sortOrder, addDistricts, addSubDistricts);
    //
    //     return Ok(result.Select(x => x.ToDto(addDistricts, addSubDistricts)));
    // }
}