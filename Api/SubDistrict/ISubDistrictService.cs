using BdGeographicalData.Shared;

namespace BdGeographicalData.Api.SubDistrict;

public interface ISubDistrictService
{
    Task<Entity.SubDistrict?> FindOneById(int id);

    Task<Entity.SubDistrict?> FindOneByEnglishName(string subDistrictName, string districtName, string divisionName);

    Task<IEnumerable<Entity.SubDistrict>> FindAll(ushort page,ushort limit, ApiResponseSortOrder sortOrder);
}