using BdGeographicalData.Domain.Entities;

namespace BdGeographicalData.Domain
{
    public interface IDivisionRepository
    {
      public Task<Division?> FindOneById(int id);

      Task<Tuple<Division?, List<District>>?> FindOneByIdWithDistrict(int id);

      Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?>
          FindOneByIdWithDistrictAndSubDistricts(int id);

    }
}
