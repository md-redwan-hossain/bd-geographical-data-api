using BdGeographicalData.Shared;

namespace BdGeographicalData.Api.SubDistrict;

public interface ISubDistrictService
{
    Task<Entity.SubDistrict?> FindOneById(int id, bool addDistrict, bool addDivision);

    Task<Entity.SubDistrict?> FindOneByEnglishName(string subDistrictName, string districtName, string divisionName,
        bool addDistrict, bool addDivision);

    Task<IEnumerable<Entity.SubDistrict>> FindAll(ApiPagination apiPagination, ApiResponseSortOrder sortOrder,
        bool addDistrict, bool addDivision);
}