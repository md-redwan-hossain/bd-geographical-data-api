using BdGeographicalData.Shared;

namespace BdGeographicalData.Api.District;

public interface IDistrictService
{
    Task<Entity.District?> FindOneById(int id, bool addDivision, bool addSubDistricts);

    Task<Entity.District?> FindOneByEnglishName(
        string districtName, string divisionName, bool addDivision, bool addSubDistricts);

    Task<IEnumerable<Entity.District>> FindAll(
        ApiPagination apiPagination, ApiResponseSortOrder sortOrder,
        bool addDivision, bool addSubDistricts);
}