namespace BdGeographicalData.Domain
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        void Save();
        Task SaveAsync();
    }
}