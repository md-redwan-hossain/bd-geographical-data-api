using BdGeographicalData.Api.Division.Dto;
using BdGeographicalData.Api.SubDistrict.Dto;

namespace BdGeographicalData.Api.District.Dto;

public class DistrictResponseDto
{
    public int Id { get; set; }
    public string EnglishName { get; set; }
    public string BanglaName { get; set; }
    public DivisionResponseDto? Division { get; set; }
    public ICollection<SubDistrictResponseDto>? SubDistricts { get; set; }
}