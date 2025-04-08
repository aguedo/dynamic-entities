using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;

namespace Dynamic.Adapters.Out.Repositories;

public class EntityTypeRepository : IEntityTypeRepository
{
    public async Task<EntityType> CreateAsync(EntityType entityType)
    {
        entityType.Id = Guid.NewGuid().ToString();
        await Task.CompletedTask;
        return entityType;
    }
}
