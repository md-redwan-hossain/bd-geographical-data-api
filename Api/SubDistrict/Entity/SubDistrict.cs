using System.ComponentModel.DataAnnotations;

namespace BdGeographicalData.Api.SubDistrict.Entity;

public class SubDistrict
{
    public int Id { get; set; }
    public int DistrictId { get; set; }
    [MaxLength(50)] public string EnglishName { get; set; }
    [MaxLength(50)] public string BanglaName { get; set; }
    public District.Entity.District District { get; set; }
}