using BdGeographicalData.Shared;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Api.SubDistrict;

public class SubDistrictService : ISubDistrictService
{
    private readonly DbSet<Entity.SubDistrict> _dbSet;

    public SubDistrictService(BdGeographicalDataDbContext dbContext) =>
        _dbSet = dbContext.Set<Entity.SubDistrict>();

    public async Task<Entity.SubDistrict?> FindOneById(int id, bool addDistrict, bool addDivision)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x => x.Id == id);
        data = IncludeRelationalData(data, addDistrict, addDivision);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<Entity.SubDistrict?> FindOneByEnglishName(
        string subDistrictName, string districtName, string divisionName, bool addDistrict, bool addDivision)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x =>
            x.EnglishName == subDistrictName
            && x.District.EnglishName == districtName
            && x.District.Division.EnglishName == divisionName);
        data = IncludeRelationalData(data, addDistrict, addDivision);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Entity.SubDistrict>> FindAll(
        ApiPagination apiPagination, ApiResponseSortOrder sortOrder, bool addDistrict, bool addDivision)
    {
        var data = _dbSet.AsQueryable();
        data = IncludeRelationalData(data, addDistrict, addDivision);

        data = data
            .Include(x => x.District)
            .ThenInclude(x => x.Division);

        if (sortOrder == ApiResponseSortOrder.Desc)
            data = data.OrderByDescending(x => x.EnglishName);
        else if (sortOrder == ApiResponseSortOrder.Asc)
            data = data.OrderBy(x => x.EnglishName);
        else
            data = data.OrderBy(x => x.Id);


        if (apiPagination.Page > 0 && apiPagination.Limit > 0)
            data = data.Skip((apiPagination.Page - 1) * apiPagination.Limit).Take(apiPagination.Limit);


        return await data.ToListAsync();
    }


    private static IQueryable<Entity.SubDistrict> IncludeRelationalData(
        IQueryable<Entity.SubDistrict> data, bool addDistrict, bool addDivision)
    {
        if (addDistrict && addDivision)
            data = data
                .Include(x => x.District)
                .ThenInclude(x => x.Division);

        else if (addDistrict && !addDivision)
            data = data.Include(x => x.District);

        return data;
    }
}