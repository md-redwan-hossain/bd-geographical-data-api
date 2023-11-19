using System.ComponentModel.DataAnnotations;
using BdGeographicalData.Api.District.Dto;
using BdGeographicalData.Api.Division.Dto;

namespace BdGeographicalData.Api.SubDistrict.Dto;

public class SubDistrictResponseDto
{
    public int Id { get; set; }
    public int DistrictId { get; set; }
    [MaxLength(50)] public string EnglishName { get; set; }
    [MaxLength(50)] public string BanglaName { get; set; }
    public DivisionResponseDto? Division { get; set; }
    public DistrictResponseDto? District { get; set; }
}