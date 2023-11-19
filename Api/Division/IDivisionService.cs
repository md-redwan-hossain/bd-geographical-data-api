using BdGeographicalData.Shared;

namespace BdGeographicalData.Api.Division;

public interface IDivisionService
{
    Task<Entity.Division?> FindOneById(int id);

    Task<Entity.Division?> FindOneByEnglishName(string divisionName);

    Task<IEnumerable<Entity.Division>> FindAll(ushort page, ushort limit, ApiResponseSortOrder sortOrder);
}