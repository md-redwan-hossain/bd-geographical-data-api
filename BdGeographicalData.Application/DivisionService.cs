using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Domain.Features;
using BdGeographicalData.Domain.Misc;

namespace BdGeographicalData.Application
{
    public class DivisionService(IApplicationUnitOfWork applicationUnitOfWork) : IDivisionService
    {
        public async Task<(Division? division, IList<(District district, List<SubDistrict> subDistricts)> relatedData)> FindOneById
            (int id, (bool districts, bool subDistricts) includeRelation)
        {
            (Division? division, IList<(District district, List<SubDistrict> subDistricts)> relatedData) entity;

            if (!includeRelation.districts && !includeRelation.subDistricts)
            {
                var data = await applicationUnitOfWork.DivisionRepository.FindOneById(id);
                entity.division = data;
                entity.relatedData = [];
                return entity;
            }

            else if (includeRelation.districts && !includeRelation.subDistricts)
            {
                var data = await applicationUnitOfWork.DivisionRepository.FindOneByIdWithDistrict(id);

                entity.division = data?.Item1;
                entity.relatedData = data?.Item2
                    .Select(district => (district, subDistricts: new List<SubDistrict>()))
                    .ToList() ?? [];

                return entity;
            }
            else
            {
                var data = await applicationUnitOfWork.DivisionRepository.FindOneByIdWithDistrictAndSubDistricts(id);

                entity.division = data?.Item1;

                entity.relatedData = data?.Item2
                    .Select(x => (x.Item1, x.Item2))
                    .ToList() ?? [];

                return entity;
            }

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
}