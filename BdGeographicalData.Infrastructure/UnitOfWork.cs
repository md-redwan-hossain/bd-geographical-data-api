using BdGeographicalData.Domain;
using Microsoft.EntityFrameworkCore;

namespace BdGeographicalData.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext) => _dbContext = dbContext;

        public virtual void Dispose() => _dbContext?.Dispose();

        public virtual async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

        public virtual void Save() => _dbContext?.SaveChanges();
        public virtual async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}
