using BdRegionalData.Api.District.Dto;

namespace BdRegionalData.Api.Division.Dto;

public class DivisionResponseDto
{
    public int Id { get; set; }
    public string EnglishName { get; set; }
    public string BanglaName { get; set; }
    public ICollection<DistrictResponseDto>? Districts { get; set; }
}