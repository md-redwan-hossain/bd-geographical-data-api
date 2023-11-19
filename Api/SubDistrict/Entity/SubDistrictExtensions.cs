using BdGeographicalData.Api.District.Dto;
using BdGeographicalData.Api.Division.Dto;
using BdGeographicalData.Api.SubDistrict.Dto;

namespace BdGeographicalData.Api.SubDistrict.Entity;

public static class SubDistrictExtensions
{
    public static SubDistrictResponseDto ToDto(this Entity.SubDistrict subDistrict, bool addDistrict,
        bool addDivision)
    {
        return new SubDistrictResponseDto
        {
            Id = subDistrict.Id,
            DistrictId = subDistrict.DistrictId,
            EnglishName = subDistrict.EnglishName,
            BanglaName = subDistrict.BanglaName,
            District = GetDistrict(),
            Division = GetDivision()
        };


        DistrictResponseDto? GetDistrict()
        {
            if (!addDistrict) return null;
            return new DistrictResponseDto
            {
                Id = subDistrict.District.Id,
                EnglishName = subDistrict.District.EnglishName,
                BanglaName = subDistrict.District.BanglaName,
            };
        }


        DivisionResponseDto? GetDivision()
        {
            if (!addDivision || !addDistrict) return null;
            return new DivisionResponseDto
            {
                Id = subDistrict.District.Division.Id,
                EnglishName = subDistrict.District.Division.EnglishName,
                BanglaName = subDistrict.District.Division.BanglaName,
            };
        }
    }
}