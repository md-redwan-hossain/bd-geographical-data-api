using BdGeographicalData.Application;
using BdGeographicalData.Domain;
using BdGeographicalData.Persistence;

namespace BdGeographicalData.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {

      public ApplicationUnitOfWork(ApplicationDbContext dbContext, IDivisionRepository divisionRepository)
       : base(dbContext)
      {
        DivisionRepository = divisionRepository;
      }

      public IDivisionRepository DivisionRepository { get; }
    }
}
