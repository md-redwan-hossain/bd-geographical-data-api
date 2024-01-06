using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Domain.Features;
using BdGeographicalData.Domain.Misc;
using BdGeographicalData.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Application;

public class DivisionService(IApplicationDbContext dbContext) : IDivisionService
{
    public Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?>
        FindOneById(int id, (bool districts, bool subDistricts) includeRelation)
    {
        var query = dbContext.Divisions
            .Where(div => div.Id == id)
            .Select(div => new Tuple<Division, List<Tuple<District, List<SubDistrict>>>>(
                div,
                dbContext.Districts
                    .Where(dist => dist.DivisionId == div.Id)
                    .Select(dist => new Tuple<District, List<SubDistrict>>(
                        dist,
                        dbContext.SubDistricts.Where(subDist => subDist.DistrictId == dist.Id).ToList()
                    ))
                    .ToList()
            )).FirstOrDefaultAsync();
        return query;
    }

    public Task<(Division division, IList<District> districts, IList<SubDistrict> subDistricts)?>
        FindOneByEnglishName(string divisionName, (bool districts, bool subDistricts) includeRelation)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Tuple<Division, IList<District>, IList<SubDistrict>>>>
        FindAll(ApiPagination apiPagination, bool districts, bool subDistricts)
    {
        throw new NotImplementedException();
    }
}