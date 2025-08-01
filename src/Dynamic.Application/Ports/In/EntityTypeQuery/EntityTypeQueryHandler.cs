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
}
