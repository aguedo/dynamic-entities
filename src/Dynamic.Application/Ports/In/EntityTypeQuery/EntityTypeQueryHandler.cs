using Dynamic.Domain.Models;
using Dynamic.Application.Ports.Out.Repositories;

namespace Dynamic.Application.Ports.In.EntityTypeQuery;

public class EntityTypeQueryHandler : IEntityTypeQueryUseCase
{
    private readonly IEntityTypeRepository _entityTypeRepository;

    public EntityTypeQueryHandler(IEntityTypeRepository entityTypeRepository)
    {
        _entityTypeRepository = entityTypeRepository;
    }

    public async Task<IEnumerable<EntityType>> GetAllAsync()
    {
        return await _entityTypeRepository.GetAllAsync();
    }

    public async Task<EntityType?> GetByIdAsync(string id)
    {
        return await _entityTypeRepository.GetByIdAsync(id);
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
