using BdGeographicalData.Domain.Entities;
using BdGeographicalData.Domain.Features;
using BdGeographicalData.Domain.Misc;

namespace BdGeographicalData.Application.Features
{
    public class DivisionService(IApplicationUnitOfWork applicationUnitOfWork) : IDivisionService
    {
        public async Task<Tuple<Division, List<Tuple<District, List<SubDistrict>>>>?> FindOneById
            (int id, (bool districts, bool subDistricts) includeRelatedData)
        {



            return await applicationUnitOfWork.DivisionRepository.FindOneById(id, includeRelatedData);
        }

        public Task<(Division division, IList<District> districts, IList<SubDistrict> subDistricts)?>
            FindOneByEnglishName(string divisionName, (bool districts, bool subDistricts) includeRelatedData)
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