using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Domain.Misc;

namespace BdGeographicalData.Domain.Features;

public interface IDivisionService
{
    Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?>
        FindOneById(int id, (bool districts, bool subDistricts) includeRelation);

    Task<(Division division, IList<District> districts, IList<SubDistrict> subDistricts)?>
        FindOneByEnglishName(string divisionName,
            (bool districts, bool subDistricts) includeRelation);

    Task<IList<Tuple<Division, IList<District>, IList<SubDistrict>>>>
        FindAll(ApiPagination apiPagination, bool districts, bool subDistricts);
}