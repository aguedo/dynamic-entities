using Dynamic.Domain.Models;

namespace Dynamic.Application.Ports.In.UpdateEntityType;

public interface IUpdateEntityTypeUseCase
{
    Task<EntityType?> UpdateAsync(string id, string? name);
}
