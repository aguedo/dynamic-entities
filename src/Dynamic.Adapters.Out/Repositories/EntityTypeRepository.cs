using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;

namespace Dynamic.Adapters.Out.Repositories;

public class EntityTypeRepository : IEntityTypeRepository
{
    private readonly List<EntityType> _entities = new();

    public Task<EntityType> CreateAsync(EntityType entityType)
    {
        entityType.Id = Guid.NewGuid().ToString();
        _entities.Add(entityType);
        return Task.FromResult(entityType);
    }

    public Task<IEnumerable<EntityType>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<EntityType>>(_entities);
    }
}
