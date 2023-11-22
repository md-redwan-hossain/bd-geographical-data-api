using BdGeographicalData.Shared;

namespace BdGeographicalData.Api.Division;

public interface IDivisionService
{
    Task<Entity.Division?> FindOneById(int id, bool addDistricts, bool addSubDistricts);

    Task<Entity.Division?> FindOneByEnglishName(string divisionName, bool addDistricts, bool addSubDistricts);

    Task<IEnumerable<Entity.Division>> FindAll(ApiPagination apiPagination, ApiResponseSortOrder sortOrder,
        bool addDistricts, bool addSubDistricts);
}