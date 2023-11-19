using BdRegionalData.Api.Division.Dto;
using BdRegionalData.Api.SubDistrict.Dto;

namespace BdRegionalData.Api.District.Dto;

public class DistrictResponseDto
{
    public int Id { get; set; }
    public string EnglishName { get; set; }
    public string BanglaName { get; set; }
    public DivisionResponseDto? Division { get; set; }
    public ICollection<SubDistrictResponseDto>? SubDistricts { get; set; }
}