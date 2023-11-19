using BdGeographicalData.Api.District.Dto;
using BdGeographicalData.Api.Division.Dto;
using BdGeographicalData.Api.SubDistrict.Dto;

namespace BdGeographicalData.Api.District.Entity;

public static class DistrictExtensions
{
    public static DistrictResponseDto ToDto(this District district, bool addDivision, bool addSubDistricts)
    {
        {
            return new DistrictResponseDto
            {
                Id = district.Id,
                EnglishName = district.EnglishName,
                BanglaName = district.BanglaName,
                Division = GetDivision(),
                SubDistricts = GetSubDistricts(),
            };

            DivisionResponseDto? GetDivision()
            {
                if (!addDivision) return null;
                return new DivisionResponseDto
                {
                    Id = district.Division.Id,
                    EnglishName = district.Division.EnglishName,
                    BanglaName = district.Division.BanglaName,
                };
            }


            List<SubDistrictResponseDto>? GetSubDistricts()
            {
                if (!addSubDistricts) return null;
                return district.SubDistricts.Select(y => new SubDistrictResponseDto
                {
                    Id = y.Id,
                    DistrictId = district.Id,
                    EnglishName = y.EnglishName,
                    BanglaName = y.BanglaName
                }).ToList();
            }
        }
    }
}