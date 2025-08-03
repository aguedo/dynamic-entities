using Dynamic.Domain.Models;
using Dynamic.Application.Ports.Out.Repositories;

namespace Dynamic.Application.Ports.In.UpdateEntityType;

public class UpdateEntityTypeHandler : IUpdateEntityTypeUseCase
{
    private readonly IEntityTypeRepository _entityTypeRepository;

    public UpdateEntityTypeHandler(IEntityTypeRepository entityTypeRepository)
    {
        _entityTypeRepository = entityTypeRepository;
    }

    public async Task<EntityType?> UpdateAsync(string id, string? name)
    {
        var entity = await _entityTypeRepository.GetByIdAsync(id);
        if (entity == null)
            return null;
        entity.Name = name;
        return await _entityTypeRepository.UpdateAsync(entity);
    }
}
