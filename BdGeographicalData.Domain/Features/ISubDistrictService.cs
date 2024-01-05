using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Domain.Misc;

namespace BdGeographicalData.Domain.Features;

public interface ISubDistrictService
{
    Task<SubDistrict?> FindOneById(int id, bool addDistrict, bool addDivision);

    Task<SubDistrict?> FindOneByEnglishName(string subDistrictName, string districtName, string divisionName,
        bool addDistrict, bool addDivision);

    Task<IEnumerable<SubDistrict>> FindAll(ApiPagination apiPagination, bool addDistrict, bool addDivision);
}