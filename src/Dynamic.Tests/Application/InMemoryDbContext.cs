using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Dynamic.Domain.Models;

namespace Dynamic.Tests.Application;

public class InMemoryDbContext : Dynamic.Adapters.Out.Repositories.IGenericDbContext
{
    private readonly List<EntityType> _entityTypes = new();

    public Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        if (entity is EntityType et)
        {
            _entityTypes.Add(et);
            return Task.FromResult(entity);
        }
        throw new System.NotSupportedException("Only EntityType is supported in this InMemoryDbContext.");
    }

    public Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
    {
        if (typeof(TEntity) == typeof(EntityType))
        {
            return Task.FromResult(_entityTypes.Cast<TEntity>());
        }
        return Task.FromResult(System.Linq.Enumerable.Empty<TEntity>());
    }
}
