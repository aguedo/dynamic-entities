using Dynamic.Application.Ports.Out.Repositories;

namespace Dynamic.Application.Ports.In.DeleteEntityType;

public class DeleteEntityTypeHandler : IDeleteEntityTypeUseCase
{
    private readonly IEntityTypeRepository _entityTypeRepository;

    public DeleteEntityTypeHandler(IEntityTypeRepository entityTypeRepository)
    {
        _entityTypeRepository = entityTypeRepository;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _entityTypeRepository.DeleteAsync(id);
    }
}
