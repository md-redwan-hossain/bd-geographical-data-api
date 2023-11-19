using BdGeographicalData.Shared;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Api.Division;

public class DivisionService : IDivisionService
{
    private readonly BdGeographicalDataDbContext _dbContext;

    public DivisionService(BdGeographicalDataDbContext dbContext) =>
        _dbContext = dbContext;


    public async Task<Entity.Division?> FindOneById(int id)
    {
        return await _dbContext.Divisions
            .Where(elem => elem.Id == id)
            .Include(x => x.Districts)
            .ThenInclude(x => x.SubDistricts)
            .FirstOrDefaultAsync();
    }

    public async Task<Entity.Division?> FindOneByEnglishName(string divisionName)
    {
        return await _dbContext.Divisions
            .Where(elem =>
                elem.EnglishName == divisionName)
            .Include(x => x.Districts)
            .ThenInclude(x => x.SubDistricts)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Entity.Division>> FindAll(ushort page, ushort limit, ApiResponseSortOrder sortOrder)
    {
        var dbSet = _dbContext.Set<Entity.Division>();
        var data = dbSet.AsQueryable();

        data = data
            .Include(x => x.Districts)
            .ThenInclude(x => x.SubDistricts);

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