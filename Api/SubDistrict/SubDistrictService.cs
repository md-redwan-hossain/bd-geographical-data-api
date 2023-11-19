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
        ushort page, ushort limit, ApiResponseSortOrder sortOrder, bool addDistrict, bool addDivision)
    {
        var data = _dbSet.AsQueryable();
        data = IncludeRelationalData(data, addDistrict, addDivision);

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


    private IQueryable<Entity.SubDistrict> IncludeRelationalData(
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