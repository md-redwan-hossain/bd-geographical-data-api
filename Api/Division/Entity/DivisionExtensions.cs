using BdGeographicalData.Api.District.Dto;
using BdGeographicalData.Api.Division.Dto;
using BdGeographicalData.Api.SubDistrict.Dto;

namespace BdGeographicalData.Api.Division.Entity;

public static class DivisionExtensions
{
    public static DivisionResponseDto ToDto(this Division division, bool addDistricts,
        bool addSubDistricts)
    {
        return new DivisionResponseDto
        {
            Id = division.Id,
            EnglishName = division.EnglishName,
            BanglaName = division.BanglaName,
            Districts = GetDistricts()
        };


        List<DistrictResponseDto>? GetDistricts()
        {
            if (!addDistricts) return null;
            return division.Districts.Select(x => new DistrictResponseDto
            {
                Id = x.Id,
                EnglishName = x.EnglishName,
                BanglaName = x.BanglaName,
                SubDistricts = GetSubDistricts(x)
            }).ToList();
        }

        List<SubDistrictResponseDto>? GetSubDistricts(District.Entity.District district)
        {
            if (!addSubDistricts) return null;
            return district.SubDistricts.Select(y => new SubDistrictResponseDto
            {
                Id = y.Id,
                EnglishName = y.EnglishName,
                BanglaName = y.BanglaName,
                DistrictId = division.Id
            }).ToList();
        }
    }
}