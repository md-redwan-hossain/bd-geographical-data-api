using BdGeographicalData.Shared;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Api.SubDistrict;

public class SubDistrictService : ISubDistrictService
{
    private readonly BdGeographicalDataDbContext _dbContext;

    public SubDistrictService(BdGeographicalDataDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Entity.SubDistrict?> FindOneById(int id)
    {
        return await _dbContext.SubDistricts
            .Where(elem => elem.Id == id)
            .Include(x => x.District)
            .ThenInclude(x => x.Division)
            .FirstOrDefaultAsync();
    }

    public async Task<Entity.SubDistrict?> FindOneByEnglishName(
        string subDistrictName, string districtName, string divisionName)
    {
        return await _dbContext.SubDistricts
            .Where(elem =>
                elem.EnglishName == subDistrictName
                && elem.District.EnglishName == districtName
                && elem.District.Division.EnglishName == divisionName)
            .Include(x => x.District)
            .ThenInclude(x => x.Division)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Entity.SubDistrict>> FindAll(
        ushort page, ushort limit, ApiResponseSortOrder sortOrder)
    {
        var dbSet = _dbContext.Set<Entity.SubDistrict>();
        var data = dbSet.AsQueryable();

        data = data
            .Include(x => x.District)
            .ThenInclude(x => x.Division);

        data = data
            .Skip((page - 1) * (limit))
            .Take(limit);

        if (sortOrder == ApiResponseSortOrder.Desc)
            data = data.OrderByDescending(x => x.EnglishName);
        else if (sortOrder == ApiResponseSortOrder.Asc)
            data = data.OrderBy(x => x.EnglishName);

        return await data.ToListAsync();
    }
}