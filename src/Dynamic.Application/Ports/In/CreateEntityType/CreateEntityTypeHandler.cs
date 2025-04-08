using Dynamic.Application.Ports.In.Shared;
using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;

namespace Dynamic.Application.Ports.In.CreateEntityType;

public class CreateEntityTypeHandler : ICreateEntityTypeUseCase
{
    private readonly IEntityTypeRepository _entityTypeRepository;

    public CreateEntityTypeHandler(IEntityTypeRepository entityTypeRepository)
    {
        _entityTypeRepository = entityTypeRepository;
    }

    public async Task<Output<CreateEntityTypeOutput>> CreateAsync(CreateEntityTypeInput createEntityType)
    {
        var entityType = new EntityType
        {
            // Map properties from createEntityType to entityType
            Name = createEntityType.Name,
        };

        if (!entityType.IsValid(out List<Error> errors))
        {
            return new Output<CreateEntityTypeOutput>(errors);
        }

        EntityType newEntityType = await _entityTypeRepository.CreateAsync(entityType);
        var output = new CreateEntityTypeOutput
        {
            // Map properties from newEntityType to output
            Id = newEntityType.Id!,
            Name = newEntityType!.Name!,
        };
        return new Output<CreateEntityTypeOutput>(output);
    }
}
