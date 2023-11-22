using BdGeographicalData.Shared;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Api.District;

public class DistrictService : IDistrictService
{
    private readonly DbSet<Entity.District> _dbSet;

    public DistrictService(BdGeographicalDataDbContext dbContext) =>
        _dbSet = dbContext.Set<Entity.District>();


    public async Task<Entity.District?> FindOneById(int id, bool addDivision, bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x => x.Id == id);
        data = IncludeRelationalData(data, addDivision, addSubDistricts);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<Entity.District?> FindOneByEnglishName(
        string districtName, string divisionName, bool addDivision, bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x =>
            x.EnglishName == districtName && x.Division.EnglishName == divisionName);
        data = IncludeRelationalData(data, addDivision, addSubDistricts);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Entity.District>> FindAll(
        ApiPagination apiPagination, ApiResponseSortOrder sortOrder,
        bool addDivision, bool addSubDistricts)
    {
        var data = _dbSet.AsQueryable();
        data = IncludeRelationalData(data, addDivision, addSubDistricts);

        data = data
            .Skip((apiPagination.Page - 1) * apiPagination.Limit)
            .Take(apiPagination.Limit);

        if (sortOrder == ApiResponseSortOrder.Desc)
            data = data.OrderByDescending(x => x.EnglishName);
        else if (sortOrder == ApiResponseSortOrder.Asc)
            data = data.OrderBy(x => x.EnglishName);

        return await data.ToListAsync();
    }

    private static IQueryable<Entity.District> IncludeRelationalData(
        IQueryable<Entity.District> data, bool addDivision, bool addSubDistricts)
    {
        if (addDivision && addSubDistricts)
            data = data
                .Include(x => x.SubDistricts)
                .Include(x => x.Division);

        else if (addDivision && !addSubDistricts)
            data = data.Include(x => x.Division);

        else if (!addDivision && addSubDistricts)
            data = data.Include(x => x.SubDistricts);

        return data;
    }
}