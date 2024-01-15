using BdGeographicalData.Domain;

namespace BdGeographicalData.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IDivisionRepository DivisionRepository { get; }

    }
}
