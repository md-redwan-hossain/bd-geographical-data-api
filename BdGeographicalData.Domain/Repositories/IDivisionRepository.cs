using BdGeographicalData.Domain.Entities;

namespace BdGeographicalData.Domain
{
    public interface IDivisionRepository
    {
        Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?>
        FindOneById(int id, (bool districts, bool subDistricts) includeRelatedData);
    }
}
