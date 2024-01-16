using BdGeographicalData.Domain;
using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Infrastructure
{
  public class DivisionRepository(ApplicationDbContext dbContext) : IDivisionRepository
  {
    public Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?>
    FindOneById(int id, (bool districts, bool subDistricts) includeRelatedData)
    {
      if (includeRelatedData.districts && includeRelatedData.subDistricts)
        return FindOneByIdWithDistrictsAndSubDistricts(id);

      else if (includeRelatedData.districts && !includeRelatedData.subDistricts)
        return FindOneByIdWithDistricts(id);

      else return FindOneByIdWithoutRelatedData(id);
    }


    private Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?> FindOneByIdWithoutRelatedData(int id)
    {
      var query = dbContext.Divisions
          .Where(div => div.Id == id)
          .Select(div => new Tuple<Division, List<Tuple<District, List<SubDistrict>>>>(
              div,
              new List<Tuple<District, List<SubDistrict>>>()
          ))
          .AsNoTracking()
          .FirstOrDefaultAsync();

      return query;

    }


    private Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?> FindOneByIdWithDistricts(int id)
    {
      var query = dbContext.Divisions
          .Where(div => div.Id == id)
          .Select(div => new Tuple<Division, List<Tuple<District, List<SubDistrict>>>>(
              div,
              dbContext.Districts
                  .Where(dist => dist.DivisionId == div.Id)
                  .Select(dist => new Tuple<District, List<SubDistrict>>(
                      dist, new List<SubDistrict>())).ToList()
          ))
          .AsNoTracking()
          .FirstOrDefaultAsync();

      return query;

    }

    private Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?> FindOneByIdWithDistrictsAndSubDistricts(int id)
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
                  )).ToList()
          ))
          .AsNoTracking()
          .FirstOrDefaultAsync();

      return query;
    }
  }
}
