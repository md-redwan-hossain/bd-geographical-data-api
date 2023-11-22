using BdGeographicalData.Shared;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Api.Division;

public class DivisionService : IDivisionService
{
    private readonly DbSet<Entity.Division> _dbSet;

    public DivisionService(BdGeographicalDataDbContext dbContext) =>
        _dbSet = dbContext.Set<Entity.Division>();

    public async Task<Entity.Division?> FindOneById(int id, bool addDistricts, bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x => x.Id == id);
        data = IncludeRelationalData(data, addDistricts, addSubDistricts);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<Entity.Division?> FindOneByEnglishName(string divisionName, bool addDistricts,
        bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x => x.EnglishName == divisionName);
        data = IncludeRelationalData(data, addDistricts, addSubDistricts);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Entity.Division>> FindAll(
        ApiPagination apiPagination, ApiResponseSortOrder sortOrder,
        bool addDistricts, bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = IncludeRelationalData(data, addDistricts, addSubDistricts);

        data = data
            .Skip((apiPagination.Page - 1) * apiPagination.Limit)
            .Take(apiPagination.Limit);

        if (sortOrder == ApiResponseSortOrder.Desc)
            data = data.OrderByDescending(x => x.EnglishName);
        else if (sortOrder == ApiResponseSortOrder.Asc)
            data = data.OrderBy(x => x.EnglishName);

        return await data.ToListAsync();
    }


    private static IQueryable<Entity.Division> IncludeRelationalData(
        IQueryable<Entity.Division> data, bool addDistricts, bool addSubDistricts)
    {
        if (addDistricts && addSubDistricts)
            data = data
                .Include(x => x.Districts)
                .ThenInclude(x => x.SubDistricts);

        else if (addDistricts && !addSubDistricts)
            data = data.Include(x => x.Districts);

        return data;
    }
}