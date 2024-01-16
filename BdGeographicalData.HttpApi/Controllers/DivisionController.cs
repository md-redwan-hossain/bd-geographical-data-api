using BdGeographicalData.Domain.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BdGeographicalData.HttpApi.Controllers
{
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
                id = result.Item1.Id,
                englishName = result.Item1.EnglishName,
                banglaName = result.Item1.BanglaName,
                districts = result.Item2.Select(district => new
                {
                    id = district.Item1.Id,
                    englishName = district.Item1.EnglishName,
                    banglaName = district.Item1.BanglaName,
                    subDistricts = district.Item2.Select(subDistrict => new
                    {
                        id = subDistrict.Id,
                        districtId = district.Item1.Id,
                        englishName = subDistrict.EnglishName,
                        banglaName = subDistrict.BanglaName
                    })
                })
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
}