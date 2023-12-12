using BdGeographicalData.Api.Division.Entity;
using BdGeographicalData.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BdGeographicalData.Api.Division;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class DivisionController(IDivisionService divisionService) : ControllerBase
{
    private const string AddDistrictsQueryKey = "add_districts";
    private const string AddSubDistrictsQueryKey = "add_sub_districts";

    [HttpGet("{division_id:int}")]
    [ResponseCache(CacheProfileName = Constants.ResponseCacheProfile,
        VaryByQueryKeys = new[] { AddDistrictsQueryKey, AddSubDistrictsQueryKey })
    ]
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
    [ResponseCache(CacheProfileName = Constants.ResponseCacheProfile,
        VaryByQueryKeys = new[] { AddDistrictsQueryKey, AddSubDistrictsQueryKey })
    ]
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
    [ResponseCache(CacheProfileName = Constants.ResponseCacheProfile,
        VaryByQueryKeys = new[]
        {
            AddDistrictsQueryKey,
            AddSubDistrictsQueryKey,
            Constants.PageQueryKey,
            Constants.LimitQueryKey,
            Constants.SortingQueryKey
        })
    ]
    public async Task<IActionResult> GetAll(
        [FromQuery] ApiPagination apiPagination,
        [FromQuery(Name = "sort_order")] ApiResponseSortOrder sortOrder,
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