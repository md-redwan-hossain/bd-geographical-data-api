// using BdGeographicalData.Domain.Entities;
// using BdGeographicalData.Domain.Features;
// using BdGeographicalData.Domain.Misc;
// using BdGeographicalData.Persistence;
// using Microsoft.EntityFrameworkCore;

// namespace BdGeographicalData.Application
// {
//     public class DistrictService(IApplicationDbContext dbContext) : IDistrictService
//     {
//         public async Task<(District district, Division? division, IList<SubDistrict> subDistricts)?> FindOneById
//             (int id, (bool division, bool subDistricts) includeRelation)
//         {
//             if (includeRelation.division)
//             {
//                 var query = await (
//                         from dist in dbContext.Districts
//                         where dist.Id == id
//                         join div in dbContext.Divisions on dist.DivisionId equals div.Id
//                         select new
//                         {
//                             dist,
//                             div,
//                             subDists = includeRelation.subDistricts
//                                 ? dbContext.SubDistricts.Where(subDist => subDist.DistrictId == dist.Id).ToList()
//                                 : new List<SubDistrict>()
//                         }
//                     ).AsNoTracking()
//                     .SingleOrDefaultAsync();

//                 return query is null ? null : (query.dist, query.div, query.subDists);
//             }

//             else
//             {
//                 var query = await dbContext.Districts.Where(dist => dist.Id == id)
//                     .Select(dist => new
//                     {
//                         dist,
//                         subDists = includeRelation.subDistricts
//                             ? dbContext.SubDistricts.Where(subDist => subDist.DistrictId == dist.Id).ToList()
//                             : new List<SubDistrict>()
//                     }).AsNoTracking()
//                     .SingleOrDefaultAsync();

//                 return query is null ? null : (query.dist, null, query.subDists);
//             }
//         }


//         public async Task<(District district, Division? division, IList<SubDistrict> subDistricts)?>
//             FindOneByEnglishName(string districtName, string divisionName,
//                 (bool division, bool subDistricts) includeRelation)
//         {
//             var query = await (
//                     from dist in dbContext.Districts
//                     where dist.EnglishName == districtName
//                     join div in dbContext.Divisions on dist.DivisionId equals div.Id
//                     where div.EnglishName == divisionName
//                     select new
//                     {
//                         dist,
//                         Division = includeRelation.division ? div : null,
//                         SubDistricts = includeRelation.subDistricts
//                             ? dbContext.SubDistricts
//                                 .Where(subDist => subDist.DistrictId == dist.Id)
//                                 .ToList()
//                             : new List<SubDistrict>()
//                     }
//                 ).AsNoTracking()
//                 .FirstOrDefaultAsync();

//             if (query is null) return null;

//             return (query.dist, query.Division, query.SubDistricts);
//         }


//         public async Task<IList<Tuple<District, Division?, IList<SubDistrict>>>>
//             FindAll(ApiPagination apiPagination,
//                 (bool division, bool subDistricts) includeRelation)
//         {
//             var query = dbContext.Districts
//                 .Select(dist => new Tuple<District, Division?, IList<SubDistrict>>(
//                     dist,
//                     includeRelation.division
//                         ? dbContext.Divisions.SingleOrDefault(div => div.Id == dist.Id)
//                         : null,
//                     includeRelation.subDistricts
//                         ? dbContext.SubDistricts.Where(subDist => subDist.DistrictId == dist.Id).ToList()
//                         : new List<SubDistrict>())
//                 );

//             if (apiPagination is { Page: > 0, Limit: > 0 })
//             {
//                 query = query.Skip((apiPagination.Page - 1) * apiPagination.Limit)
//                     .Take(apiPagination.Limit);
//             }

//             return await query.ToListAsync();
//         }
//     }
// }