using BdGeographicalData.Domain.Entities;

namespace BdGeographicalData.Domain
{
    public interface IDivisionRepository
    {
      public Task<Division?> FindOneById(int id);

      Task<Tuple<Division?, IList<District>>?> FindOneByIdWithDistrict(int id);

      Task<Tuple<Division, IList<Tuple<District, IList<SubDistrict>>>>?>
          FindOneByIdWithDistrictAndSubDistricts(int id);

    }
}
