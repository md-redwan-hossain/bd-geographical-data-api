using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Domain.Misc;

namespace BdGeographicalData.Domain.Features
{
    public interface IDivisionService
    {
        // Task<Tuple<Division?, IList<Tuple<District, IList<SubDistrict>>>>>

        Task<(Division? division, IList<(District district, List<SubDistrict> subDistricts)> relatedData)> FindOneById
            (int id, (bool districts, bool subDistricts) includeRelation);

        Task<(Division division, IList<District> districts, IList<SubDistrict> subDistricts)?>
            FindOneByEnglishName(string divisionName,
                (bool districts, bool subDistricts) includeRelation);

        Task<IList<Tuple<Division, IList<District>, IList<SubDistrict>>>>
            FindAll(ApiPagination apiPagination, bool districts, bool subDistricts);
    }
}