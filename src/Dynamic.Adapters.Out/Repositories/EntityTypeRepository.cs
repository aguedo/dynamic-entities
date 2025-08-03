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

    public Task<EntityType?> GetByIdAsync(string id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entity);
    }

    public Task<EntityType?> UpdateAsync(EntityType entityType)
    {
        var existing = _entities.FirstOrDefault(e => e.Id == entityType.Id);
        if (existing == null)
            return Task.FromResult<EntityType?>(null);
        existing.Name = entityType.Name;
        // Add field updates here if needed
        return Task.FromResult<EntityType?>(existing);
    }
}
