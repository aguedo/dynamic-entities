using Dynamic.Domain.Models;

namespace Dynamic.Application.Ports.In.EntityTypeQuery;

public interface IEntityTypeQueryUseCase
{
    Task<IEnumerable<EntityType>> GetAllAsync();
    Task<EntityType?> GetByIdAsync(string id);
    Task<EntityType?> UpdateAsync(string id, string? name);
}
