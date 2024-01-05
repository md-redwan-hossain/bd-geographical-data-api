using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Domain.Misc;

namespace BdGeographicalData.Domain.Features;

public interface IDivisionService
{
    Task<Division?> FindOneById(int id, bool addDistricts, bool addSubDistricts);

    Task<Division?> FindOneByEnglishName(string divisionName, bool addDistricts, bool addSubDistricts);

    Task<IEnumerable<Division>> FindAll(ApiPagination apiPagination,
        bool addDistricts, bool addSubDistricts);
}