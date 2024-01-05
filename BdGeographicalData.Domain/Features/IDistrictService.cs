using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Domain.Misc;

namespace BdGeographicalData.Domain.Features;

public interface IDistrictService
{
    Task<(District district, Division? division, IList<SubDistrict> subDistricts)?> FindOneById
        (int id, (bool division, bool subDistricts) includeRelation);

    Task<(District district, Division? division, IList<SubDistrict> subDistricts)?> FindOneByEnglishName
        (string districtName, string divisionName, (bool division, bool subDistricts) includeRelation);

    Task<IList<Tuple<District, Division?, IList<SubDistrict>>>> FindAll
        (ApiPagination apiPagination, (bool division, bool subDistricts) includeRelation);
}