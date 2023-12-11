using BdGeographicalData.Shared;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Api.Division;

public class DivisionService(BdGeographicalDataDbContext dbContext) : IDivisionService
{
    private readonly DbSet<Entity.Division> _dbSet = dbContext.Set<Entity.Division>();

    public Task<Entity.Division?> FindOneById(int id, bool addDistricts, bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x => x.Id == id);
        data = IncludeRelationalData(data, addDistricts, addSubDistricts);
        return data.FirstOrDefaultAsync();
    }


    public Task<Entity.Division?> FindOneByEnglishName(string divisionName, bool addDistricts,
        bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x => x.EnglishName == divisionName);
        data = IncludeRelationalData(data, addDistricts, addSubDistricts);
        return data.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Entity.Division>> FindAll(
        ApiPagination apiPagination, ApiResponseSortOrder sortOrder,
        bool addDistricts, bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = IncludeRelationalData(data, addDistricts, addSubDistricts);

        data = sortOrder switch
        {
            ApiResponseSortOrder.Desc => data.OrderByDescending(x => x.EnglishName),
            ApiResponseSortOrder.Asc => data.OrderBy(x => x.EnglishName),
            _ => data.OrderBy(x => x.Id)
        };

        if (apiPagination is { Page: > 0, Limit: > 0 })
            data = data.Skip((apiPagination.Page - 1) * apiPagination.Limit).Take(apiPagination.Limit);

        return await data.ToListAsync();
    }


    private static IQueryable<Entity.Division> IncludeRelationalData(
        IQueryable<Entity.Division> data, bool addDistricts, bool addSubDistricts)
    {
        if (!addDistricts) return data;

        return addSubDistricts switch
        {
            true => data.Include(x => x.Districts).ThenInclude(x => x.SubDistricts),
            false => data.Include(x => x.Districts)
        };
    }
}