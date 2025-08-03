
using Dynamic.Domain.Models;

namespace Dynamic.Application.Ports.Out.Repositories;

public interface IEntityTypeRepository
{
    Task<EntityType> CreateAsync(EntityType entityType);
    Task<IEnumerable<EntityType>> GetAllAsync();
    Task<EntityType?> GetByIdAsync(string id);
    Task<EntityType?> UpdateAsync(EntityType entityType);
}
