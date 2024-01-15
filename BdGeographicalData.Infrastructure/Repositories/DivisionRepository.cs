using BdGeographicalData.Domain;
using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Infrastructure
{
  public class DivisionRepository(ApplicationDbContext dbContext) : IDivisionRepository
  {
    public async Task<Division?> FindOneById(int id)
    {
      return await dbContext.Divisions
      .Where(div => div.Id == id)
      .AsNoTracking()
      .FirstOrDefaultAsync();
    }

    public Task<Tuple<Division?, List<District>>?> FindOneByIdWithDistrict(int id)
    {
      var query = dbContext.Divisions
       .Where(div => div.Id == id)
       .Select(div => new Tuple<Division?, List<District>>(
           div,
           dbContext.Districts
           .Where(dist => dist.DivisionId == div.Id)
           .ToList()
       ))
       .AsNoTracking()
       .FirstOrDefaultAsync();

      return query;
    }

    public Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?>
     FindOneByIdWithDistrictAndSubDistricts(int id)
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
