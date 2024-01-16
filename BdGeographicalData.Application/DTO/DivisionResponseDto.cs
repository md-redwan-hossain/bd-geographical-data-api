using BdGeographicalData.Domain.Entities;

namespace BdGeographicalData.Application;

public class DivisionResponseDto
{
  public int Id { get; set; }
  public string EnglishName { get; set; }
  public string BanglaName { get; set; }
  public List<DistrictResponseDto> Districts { get; set; }


  public static DivisionResponseDto Response(Tuple<Division, List<Tuple<District, List<SubDistrict>>>> data)
  {
    return new DivisionResponseDto
    {
      Id = data.Item1.Id,
      EnglishName = data.Item1.EnglishName,
      BanglaName = data.Item1.BanglaName,
      Districts = data.Item2.Select(district => new DistrictResponseDto
      {
        Id = district.Item1.Id,
        EnglishName = district.Item1.EnglishName,
        BanglaName = district.Item1.BanglaName,
        SubDistricts = district.Item2.Select(subDistrict => new SubDistrictResponseDto
        {
          Id = subDistrict.Id,
          DistrictId = district.Item1.Id,
          EnglishName = subDistrict.EnglishName,
          BanglaName = subDistrict.BanglaName
        }).ToList()
      }).ToList()
    };
  }
}
