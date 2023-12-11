using BdGeographicalData.Shared;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Api.SubDistrict;

public class SubDistrictService(BdGeographicalDataDbContext dbContext) : ISubDistrictService
{
    private readonly DbSet<Entity.SubDistrict> _dbSet = dbContext.Set<Entity.SubDistrict>();

    public Task<Entity.SubDistrict?> FindOneById(int id, bool addDistrict, bool addDivision)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x => x.Id == id);
        data = IncludeRelationalData(data, addDistrict, addDivision);
        return data.FirstOrDefaultAsync();
    }

    public Task<Entity.SubDistrict?> FindOneByEnglishName(
        string subDistrictName, string districtName, string divisionName, bool addDistrict, bool addDivision)
    {
        var data = _dbSet.AsQueryable();
        data = data.Where(x =>
            x.EnglishName == subDistrictName
            && x.District.EnglishName == districtName
            && x.District.Division.EnglishName == divisionName);
        data = IncludeRelationalData(data, addDistrict, addDivision);
        return data.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Entity.SubDistrict>> FindAll(
        ApiPagination apiPagination, ApiResponseSortOrder sortOrder, bool addDistrict, bool addDivision)
    {
        var data = _dbSet.AsQueryable();
        data = IncludeRelationalData(data, addDistrict, addDivision);

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

    private static IQueryable<Entity.SubDistrict> IncludeRelationalData(
        IQueryable<Entity.SubDistrict> data, bool addDistrict, bool addDivision)
    {
        if (!addDistrict) return data;

        return addDivision switch
        {
            true => data.Include(x => x.District).ThenInclude(x => x.Division),
            false => data.Include(x => x.District)
        };
    }
}